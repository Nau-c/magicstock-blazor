// 🚀 MagicStock Service Worker v1.1
// Implementa cache inteligente, sincronización offline y notificaciones push

const CACHE_NAME = 'magicstock-v1.1.0';
const STATIC_CACHE = 'magicstock-static-v1.1.0';
const API_CACHE = 'magicstock-api-v1.1.0';
const IMAGE_CACHE = 'magicstock-images-v1.1.0';

// 📦 Recursos estáticos para cache
const STATIC_RESOURCES = [
    '/',
    '/index.html',
    '/manifest.json',
    '/icon-192.png',
    '/icon-512.png',
    '/_framework/blazor.webassembly.js',
    '/_framework/dotnet.wasm',
    '/_content/MudBlazor/MudBlazor.min.css',
    '/_content/MudBlazor/MudBlazor.min.js'
];

// 🌐 URLs de API para cache
const API_PATTERNS = [
    /^https:\/\/api\.scryfall\.com\/cards\/search/,
    /^https:\/\/api\.scryfall\.com\/cards\/[a-f0-9-]+$/,
    /^https:\/\/api\.scryfall\.com\/sets/
];

// 🖼️ Patrones de imágenes para cache
const IMAGE_PATTERNS = [
    /^https:\/\/cards\.scryfall\.io\//,
    /^https:\/\/c1\.scryfall\.com\//
];

// 📱 Instalación del Service Worker
self.addEventListener('install', event => {
    console.log('🔧 Instalando MagicStock Service Worker v1.1');

    event.waitUntil(
        Promise.all([
            // Cache de recursos estáticos
            caches.open(STATIC_CACHE).then(cache => {
                console.log('📦 Cacheando recursos estáticos');
                return cache.addAll(STATIC_RESOURCES);
            }),

            // Forzar activación inmediata
            self.skipWaiting()
        ])
    );
});

// 🔄 Activación del Service Worker
self.addEventListener('activate', event => {
    console.log('✅ Activando MagicStock Service Worker v1.1');

    event.waitUntil(
        Promise.all([
            // Limpiar caches antiguos
            cleanupOldCaches(),

            // Tomar control inmediato
            self.clients.claim()
        ])
    );
});

// 🌐 Interceptar requests
self.addEventListener('fetch', event => {
    const { request } = event;
    const url = new URL(request.url);

    // Estrategias de cache según el tipo de recurso
    if (isStaticResource(request)) {
        event.respondWith(cacheFirst(request, STATIC_CACHE));
    } else if (isApiRequest(request)) {
        event.respondWith(networkFirstWithFallback(request, API_CACHE));
    } else if (isImageRequest(request)) {
        event.respondWith(cacheFirstWithExpiry(request, IMAGE_CACHE, 7 * 24 * 60 * 60 * 1000)); // 7 días
    } else {
        event.respondWith(networkFirst(request));
    }
});

// 🔄 Sincronización en background
self.addEventListener('sync', event => {
    console.log('🔄 Background sync:', event.tag);

    if (event.tag === 'stock-sync') {
        event.waitUntil(syncStockData());
    } else if (event.tag === 'price-update') {
        event.waitUntil(updatePrices());
    }
});

// 🔔 Notificaciones push
self.addEventListener('push', event => {
    if (!event.data) return;

    const data = event.data.json();
    const options = {
        body: data.body,
        icon: '/icon-192.png',
        badge: '/icon-192.png',
        tag: data.tag || 'magicstock-notification',
        data: data.data,
        actions: [
            {
                action: 'view',
                title: 'Ver',
                icon: '/icon-192.png'
            },
            {
                action: 'dismiss',
                title: 'Descartar'
            }
        ],
        requireInteraction: data.requireInteraction || false
    };

    event.waitUntil(
        self.registration.showNotification(data.title, options)
    );
});

// 🎯 Click en notificaciones
self.addEventListener('notificationclick', event => {
    event.notification.close();

    if (event.action === 'view') {
        const url = event.notification.data?.url || '/';
        event.waitUntil(
            clients.openWindow(url)
        );
    }
});

// 🛠️ Funciones auxiliares

async function cacheFirst(request, cacheName) {
    try {
        const cache = await caches.open(cacheName);
        const cached = await cache.match(request);

        if (cached) {
            return cached;
        }

        const response = await fetch(request);
        if (response.ok) {
            cache.put(request, response.clone());
        }

        return response;
    } catch (error) {
        console.error('❌ Cache first error:', error);
        return new Response('Offline', { status: 503 });
    }
}

async function networkFirstWithFallback(request, cacheName) {
    try {
        const response = await fetch(request);

        if (response.ok) {
            const cache = await caches.open(cacheName);
            cache.put(request, response.clone());
        }

        return response;
    } catch (error) {
        console.log('🌐 Network failed, trying cache:', request.url);

        const cache = await caches.open(cacheName);
        const cached = await cache.match(request);

        if (cached) {
            return cached;
        }

        // Fallback para APIs críticas
        if (request.url.includes('/cards/search')) {
            return new Response(JSON.stringify({
                object: 'list',
                total_cards: 0,
                has_more: false,
                data: []
            }), {
                headers: { 'Content-Type': 'application/json' }
            });
        }

        return new Response('Offline', { status: 503 });
    }
}

async function cacheFirstWithExpiry(request, cacheName, maxAge) {
    try {
        const cache = await caches.open(cacheName);
        const cached = await cache.match(request);

        if (cached) {
            const cachedDate = new Date(cached.headers.get('sw-cached-date'));
            const now = new Date();

            if (now - cachedDate < maxAge) {
                return cached;
            }
        }

        const response = await fetch(request);
        if (response.ok) {
            const responseToCache = response.clone();
            responseToCache.headers.set('sw-cached-date', new Date().toISOString());
            cache.put(request, responseToCache);
        }

        return response;
    } catch (error) {
        const cache = await caches.open(cacheName);
        const cached = await cache.match(request);
        return cached || new Response('Offline', { status: 503 });
    }
}

async function networkFirst(request) {
    try {
        return await fetch(request);
    } catch (error) {
        return new Response('Offline', { status: 503 });
    }
}

function isStaticResource(request) {
    return STATIC_RESOURCES.some(resource =>
        request.url.endsWith(resource) || request.url.includes('_framework')
    );
}

function isApiRequest(request) {
    return API_PATTERNS.some(pattern => pattern.test(request.url));
}

function isImageRequest(request) {
    return IMAGE_PATTERNS.some(pattern => pattern.test(request.url)) ||
        request.destination === 'image';
}

async function cleanupOldCaches() {
    const cacheNames = await caches.keys();
    const oldCaches = cacheNames.filter(name =>
        name.startsWith('magicstock-') &&
        !name.includes('v1.1.0')
    );

    return Promise.all(
        oldCaches.map(name => {
            console.log('🗑️ Eliminando cache antiguo:', name);
            return caches.delete(name);
        })
    );
}

async function syncStockData() {
    try {
        console.log('🔄 Sincronizando datos de stock...');

        // Obtener datos pendientes del IndexedDB
        const pendingData = await getPendingStockUpdates();

        if (pendingData.length > 0) {
            // Enviar al servidor cuando haya conexión
            const response = await fetch('/api/stock/sync', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(pendingData)
            });

            if (response.ok) {
                await clearPendingStockUpdates();
                console.log('✅ Stock sincronizado correctamente');
            }
        }
    } catch (error) {
        console.error('❌ Error sincronizando stock:', error);
    }
}

async function updatePrices() {
    try {
        console.log('💰 Actualizando precios...');

        // Lógica para actualizar precios en background
        const response = await fetch('/api/prices/update', {
            method: 'POST'
        });

        if (response.ok) {
            console.log('✅ Precios actualizados');

            // Notificar al usuario si hay cambios significativos
            const data = await response.json();
            if (data.significantChanges) {
                self.registration.showNotification('💰 Precios Actualizados', {
                    body: `${data.changedCards} cartas han cambiado de precio`,
                    icon: '/icon-192.png',
                    tag: 'price-update'
                });
            }
        }
    } catch (error) {
        console.error('❌ Error actualizando precios:', error);
    }
}

// Funciones de IndexedDB (simplificadas)
async function getPendingStockUpdates() {
    // Implementar acceso a IndexedDB
    return [];
}

async function clearPendingStockUpdates() {
    // Implementar limpieza de IndexedDB
}

console.log('🚀 MagicStock Service Worker v1.1 cargado correctamente'); 