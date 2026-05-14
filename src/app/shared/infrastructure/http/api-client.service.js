const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || "https://lowcortisol-platform.onrender.com";

function buildUrl(path, query = {}) {
    const url = new URL(`${API_BASE_URL}${path}`);

    Object.entries(query).forEach(([key, value]) => {
        if (value !== undefined && value !== null && value !== "") {
            url.searchParams.set(key, value);
        }
    });

    return url.toString();
}

async function request(path, options = {}) {
    const response = await fetch(buildUrl(path, options.query), {
        method: options.method || "GET",
        headers: {
            "Content-Type": "application/json",
            ...(options.headers || {}),
        },
        body: options.body ? JSON.stringify(options.body) : undefined,
    });

    if (!response.ok) {
        const message = await response.text();
        throw new Error(message || "No se pudo completar la solicitud.");
    }

    if (response.status === 204) {
        return null;
    }

    return response.json();
}

export class ApiClientService {
    static get(path, query = {}) {
        return request(path, {
            method: "GET",
            query,
        });
    }

    static post(path, body = {}) {
        return request(path, {
            method: "POST",
            body,
        });
    }

    static patch(path, body = {}) {
        return request(path, {
            method: "PATCH",
            body,
        });
    }

    static put(path, body = {}) {
        return request(path, {
            method: "PUT",
            body,
        });
    }

    static delete(path) {
        return request(path, {
            method: "DELETE",
        });
    }
}