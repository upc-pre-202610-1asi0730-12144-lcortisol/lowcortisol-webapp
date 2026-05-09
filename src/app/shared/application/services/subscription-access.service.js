import { ApiClientService } from "../../infrastructure/http/api-client.service";
import { AuthSessionService } from "./auth-session.service";

export class SubscriptionAccessService {
    static async hasActiveSubscription() {
        const subscription = await SubscriptionAccessService.getActiveSubscription();

        return Boolean(subscription && subscription.status === "active" && subscription.planId);
    }

    static async getActiveSubscription() {
        const user = AuthSessionService.getCurrentUser();

        if (!user) {
            return null;
        }

        const subscriptions = await ApiClientService.get("/subscriptions", {
            userId: user.id,
            status: "active",
        });

        return subscriptions[0] || null;
    }
}