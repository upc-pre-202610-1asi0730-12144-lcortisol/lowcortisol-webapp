import DevicePage from "../pages/device/device-page.component.vue";

export const deviceControlRoutes = [
    {
        path: "/devices",
        name: "devices",
        component: DevicePage,
        meta: {
            requiresAuth: true,
            requiresSubscription: true,
        },
    },
];