import { createRouter, createWebHistory } from "vue-router";
import { appRoutes } from "../app/app.routes";
import { AuthSessionService } from "../app/shared/application/services/auth-session.service";
import { SubscriptionAccessService } from "../app/shared/application/services/subscription-access.service";

const router = createRouter({
    history: createWebHistory(),
    routes: appRoutes,
    scrollBehavior() {
        return {
            top: 0,
            behavior: "smooth",
        };
    },
});

router.beforeEach(async (to) => {
    const isPublicRoute = Boolean(to.meta.public);
    const requiresAuth = Boolean(to.meta.requiresAuth);
    const requiresSubscription = Boolean(to.meta.requiresSubscription);

    const isAuthenticated = AuthSessionService.isAuthenticated();

    if (requiresAuth && !isAuthenticated) {
        return {
            name: "login",
        };
    }

    if (to.name === "login" && isAuthenticated) {
        const hasActiveSubscription = await SubscriptionAccessService.hasActiveSubscription();

        return {
            name: hasActiveSubscription ? "dashboard" : "plans",
        };
    }

    if (requiresSubscription) {
        const hasActiveSubscription = await SubscriptionAccessService.hasActiveSubscription();

        if (!hasActiveSubscription) {
            return {
                name: "plans",
            };
        }
    }

    if (isPublicRoute) {
        return true;
    }

    return true;
});

export default router;