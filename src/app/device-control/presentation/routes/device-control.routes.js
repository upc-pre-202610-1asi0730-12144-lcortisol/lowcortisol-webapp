import DevicesPage from "../pages/devices/devices-page.component.vue";

export const deviceControlRoutes = [
    {
        path: "/devices",
        name: "devices",
        component: DevicesPage,
        meta: {
            requiresAuth: true,
            requiresSubscription: true,
        },
    },
];