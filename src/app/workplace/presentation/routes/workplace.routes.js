import SitesPage from "../pages/sites/sites-page.component.vue";

export const workplaceRoutes = [
    {
        path: "/sites",
        name: "sites",
        component: SitesPage,
        meta: {
            requiresAuth: true,
            requiresSubscription: true,
        },
    },
];