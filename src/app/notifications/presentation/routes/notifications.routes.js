import AlertsPage from "../pages/alerts/alerts-page.component.vue";

export const notificationsRoutes = [
    {
        path: "/alerts",
        name: "alerts",
        component: AlertsPage,
        meta: {
            requiresAuth: true,
            requiresSubscription: true,
        },
    },
];