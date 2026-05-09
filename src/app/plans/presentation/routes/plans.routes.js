import PlansPage from "../pages/plans/plans-page.component.vue";

export const plansRoutes = [
    {
        path: "/plans",
        name: "plans",
        component: PlansPage,
        meta: {
            requiresAuth: true,
        },
    },
];