import { ApiClientService } from "../../../shared/infrastructure/http/api-client.service";
import { AuthSessionService } from "../../../shared/application/services/auth-session.service";

function normalizeUser(user) {
    if (!user) return null;

    const names = String(user.fullName || "")
        .split(" ")
        .filter(Boolean);

    const initials = names
        .slice(0, 2)
        .map((name) => name[0])
        .join("")
        .toUpperCase();

    return {
        ...user,
        initials: initials || "U",
        statusLabel: user.status === "active" ? "Activo" : "Inactivo",
    };
}

function normalizeAccessProfile(profile) {
    if (!profile) return null;

    const roleLabels = {
        owner: "Propietario",
        admin: "Administrador",
        operator: "Operador",
        viewer: "Visualizador",
    };

    return {
        ...profile,
        roleLabel: roleLabels[profile.role] || profile.role || "Sin rol",
        permissions: profile.permissions || [],
    };
}

export class IamApiService {
    async signIn(payload) {
        const users = await ApiClientService.get("/users", {
            email: payload.email,
            password: payload.password,
        });

        const user = users[0];

        if (!user) {
            throw new Error("Correo o contraseña incorrectos.");
        }

        if (user.status !== "active") {
            throw new Error("La cuenta no está activa.");
        }

        const accessProfiles = await ApiClientService.get("/accessProfiles", {
            id: user.accessProfileId,
        });

        const accessProfile = accessProfiles[0] || null;

        const session = {
            id: `SESSION-${Date.now()}`,
            userId: user.id,
            token: `fake-token-${Date.now()}`,
            isActive: true,
            createdAt: new Date().toISOString(),
        };

        AuthSessionService.signIn(normalizeUser(user));

        return {
            user: normalizeUser(user),
            accessProfile: normalizeAccessProfile(accessProfile),
            session,
            message: "Inicio de sesión correcto.",
        };
    }

    async signUp(payload) {
        const existingUsers = await ApiClientService.get("/users", {
            email: payload.email,
        });

        if (existingUsers.length > 0) {
            throw new Error("Ya existe una cuenta registrada con este correo.");
        }

        const user = await ApiClientService.post("/users", {
            fullName: payload.fullName,
            email: payload.email,
            phone: payload.phone,
            password: payload.password,
            status: "active",
            accessProfileId: "PROFILE-OWNER",
            createdAt: new Date().toISOString(),
            updatedAt: new Date().toISOString(),
        });

        const accessProfiles = await ApiClientService.get("/accessProfiles", {
            id: "PROFILE-OWNER",
        });

        const accessProfile = accessProfiles[0] || null;

        const session = {
            id: `SESSION-${Date.now()}`,
            userId: user.id,
            token: `fake-token-${Date.now()}`,
            isActive: true,
            createdAt: new Date().toISOString(),
        };

        AuthSessionService.signIn(normalizeUser(user));

        return {
            user: normalizeUser(user),
            accessProfile: normalizeAccessProfile(accessProfile),
            session,
            message: "Cuenta creada correctamente.",
        };
    }

    async recoverPassword(payload) {
        const users = await ApiClientService.get("/users", {
            email: payload.email,
        });

        if (users.length === 0) {
            throw new Error("No existe una cuenta registrada con ese correo.");
        }

        return {
            success: true,
            message: `Se enviaron instrucciones de recuperación a ${payload.email}.`,
        };
    }

    async signOut() {
        AuthSessionService.signOut();

        return {
            success: true,
            message: "Sesión cerrada correctamente.",
        };
    }

    async getProfile() {
        const currentUser = AuthSessionService.getCurrentUser();

        if (!currentUser) {
            return null;
        }

        const user = await ApiClientService.get(`/users/${currentUser.id}`);

        AuthSessionService.setCurrentUser(normalizeUser(user));

        return normalizeUser(user);
    }

    async getAccessProfile() {
        const currentUser = AuthSessionService.getCurrentUser();

        if (!currentUser) {
            return null;
        }

        const profiles = await ApiClientService.get("/accessProfiles", {
            id: currentUser.accessProfileId,
        });

        return normalizeAccessProfile(profiles[0] || null);
    }

    async getSession() {
        const currentUser = AuthSessionService.getCurrentUser();

        if (!currentUser) {
            return null;
        }

        return {
            id: `SESSION-${currentUser.id}`,
            userId: currentUser.id,
            token: "fake-token-current-session",
            isActive: true,
            canAccess: true,
        };
    }

    async updateProfile(payload) {
        const currentUser = AuthSessionService.getCurrentUser();

        if (!currentUser) {
            throw new Error("No hay sesión activa.");
        }

        const updatedUser = await ApiClientService.patch(`/users/${currentUser.id}`, {
            fullName: payload.fullName,
            phone: payload.phone,
            updatedAt: new Date().toISOString(),
        });

        AuthSessionService.setCurrentUser(normalizeUser(updatedUser));

        return normalizeUser(updatedUser);
    }
}