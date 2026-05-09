import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";
import { AuthSessionService } from "../../../shared/application/services/auth-session.service";

function getCurrentUserId() {
    const user = AuthSessionService.getCurrentUser();

    if (!user) {
        throw new Error("No hay sesión activa.");
    }

    return user.id;
}

export class SupportApiService {
    async getTickets() {
        const [tickets, messages] = await Promise.all([
            ApiClientService.get("/supportTickets"),
            ApiClientService.get("/supportMessages"),
        ]);

        return tickets.map((ticket) => ({
            ...ticket,
            messages: messages.filter((message) => message.ticketId === ticket.id),
        }));
    }

    async createTicket(payload) {
        const userId = getCurrentUserId();

        return ApiClientService.post("/supportTickets", {
            userId,
            siteId: payload.siteId || "SITE-001",
            title: payload.title,
            description: payload.description,
            category: payload.category || "technical",
            priority: payload.category === "device" ? "high" : "medium",
            status: payload.category === "device" ? "assigned" : "open",
            assignedAgentId: payload.category === "device" ? "AGENT-002" : "",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async resolveTicket(ticketId) {
        return ApiClientService.patch(`/supportTickets/${ticketId}`, {
            status: "resolved",
            updatedAt: new Date().toISOString(),
        });
    }

    async closeTicket(ticketId) {
        return ApiClientService.patch(`/supportTickets/${ticketId}`, {
            status: "closed",
            updatedAt: new Date().toISOString(),
        });
    }

    async sendMessage(payload) {
        const userId = getCurrentUserId();

        return ApiClientService.post("/supportMessages", {
            ticketId: payload.ticketId,
            senderId: userId,
            senderType: "user",
            content: payload.content,
            status: "sent",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });
    }

    async getAgents() {
        return ApiClientService.get("/supportAgents");
    }

    async getArticles() {
        return ApiClientService.get("/knowledgeArticles");
    }

    async getConversations() {
        const [conversations, messages] = await Promise.all([
            ApiClientService.get("/supportConversations"),
            ApiClientService.get("/supportMessages"),
        ]);

        return conversations.map((conversation) => ({
            ...conversation,
            messages: messages.filter((message) => message.ticketId === conversation.ticketId),
        }));
    }

    async getSummary() {
        const [tickets, agents, articles, conversations] = await Promise.all([
            ApiClientService.get("/supportTickets"),
            ApiClientService.get("/supportAgents"),
            ApiClientService.get("/knowledgeArticles"),
            ApiClientService.get("/supportConversations"),
        ]);

        return {
            totalTickets: tickets.length,
            openTickets: tickets.filter((ticket) => ticket.status === "open").length,
            assignedTickets: tickets.filter((ticket) => ticket.status === "assigned").length,
            resolvedTickets: tickets.filter((ticket) => ticket.status === "resolved").length,
            availableAgents: agents.filter((agent) => agent.status === "available").length,
            totalArticles: articles.length,
            totalConversations: conversations.length,
        };
    }
}