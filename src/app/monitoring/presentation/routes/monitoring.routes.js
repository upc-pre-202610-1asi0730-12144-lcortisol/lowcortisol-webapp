import DashboardPage from "../pages/dashboard/dashboard-page.component.vue";
import ReportsPage from "../pages/reports/reports-page.component.vue";

export const monitoringRoutes = [
    {
        path: "/dashboard",
        name: "dashboard",
        component: DashboardPage,
        meta: {
            requiresAuth: true,
            requiresSubscription: true,
        },
    },
    {
        path: "/reports",
        name: "reports",
        component: ReportsPage,
        meta: {
            requiresAuth: true,
            requiresSubscription: true,
        },
    },
];