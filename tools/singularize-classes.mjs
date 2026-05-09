import fs from "fs";
import path from "path";

const root = process.cwd();

const codeExtensions = new Set([
    ".js",
    ".vue",
    ".ts",
    ".jsx",
    ".tsx",
    ".json",
]);

const excludedDirs = new Set([
    "node_modules",
    ".git",
    "dist",
    "build",
]);

const pathRenames = [
    // Bounded context folders
    ["src/app/plans", "src/app/plan"],
    ["src/app/notifications", "src/app/notification"],

    // Plans
    ["src/app/plan/application/store/plans.store.js", "src/app/plan/application/store/plan.store.js"],
    ["src/app/plan/application/services/plans.facade.js", "src/app/plan/application/services/plan.facade.js"],
    ["src/app/plan/infrastructure/api/plans-api.service.js", "src/app/plan/infrastructure/api/plan-api.service.js"],
    ["src/app/plan/infrastructure/api/plans-api.endpoint.js", "src/app/plan/infrastructure/api/plan-api.endpoint.js"],
    ["src/app/plan/presentation/routes/plans.routes.js", "src/app/plan/presentation/routes/plan.routes.js"],
    ["src/app/plan/presentation/pages/plans", "src/app/plan/presentation/pages/plan"],
    ["src/app/plan/presentation/pages/plan/plans-page.component.vue", "src/app/plan/presentation/pages/plan/plan-page.component.vue"],

    // Notifications
    ["src/app/notification/application/store/notifications.store.js", "src/app/notification/application/store/notification.store.js"],
    ["src/app/notification/application/services/notifications.facade.js", "src/app/notification/application/services/notification.facade.js"],
    ["src/app/notification/infrastructure/api/notifications-api.service.js", "src/app/notification/infrastructure/api/notification-api.service.js"],
    ["src/app/notification/infrastructure/api/alerts-api.endpoint.js", "src/app/notification/infrastructure/api/alert-api.endpoint.js"],
    ["src/app/notification/presentation/routes/notifications.routes.js", "src/app/notification/presentation/routes/notification.routes.js"],
    ["src/app/notification/presentation/pages/alerts", "src/app/notification/presentation/pages/alert"],
    ["src/app/notification/presentation/pages/alert/alerts-page.component.vue", "src/app/notification/presentation/pages/alert/alert-page.component.vue"],

    // Workplace pages/endpoints
    ["src/app/workplace/presentation/pages/sites", "src/app/workplace/presentation/pages/site"],
    ["src/app/workplace/presentation/pages/site/sites-page.component.vue", "src/app/workplace/presentation/pages/site/site-page.component.vue"],
    ["src/app/workplace/infrastructure/api/sites-api.service.js", "src/app/workplace/infrastructure/api/site-api.service.js"],
    ["src/app/workplace/infrastructure/api/sites-api.endpoint.js", "src/app/workplace/infrastructure/api/site-api.endpoint.js"],

    // Device Control pages/endpoints
    ["src/app/device-control/presentation/pages/devices", "src/app/device-control/presentation/pages/device"],
    ["src/app/device-control/presentation/pages/device/devices-page.component.vue", "src/app/device-control/presentation/pages/device/device-page.component.vue"],
    ["src/app/device-control/infrastructure/api/devices-api.service.js", "src/app/device-control/infrastructure/api/device-api.service.js"],
    ["src/app/device-control/infrastructure/api/devices-api.endpoint.js", "src/app/device-control/infrastructure/api/device-api.endpoint.js"],
    ["src/app/device-control/infrastructure/api/sensors-api.service.js", "src/app/device-control/infrastructure/api/sensor-api.service.js"],
    ["src/app/device-control/infrastructure/api/valves-api.service.js", "src/app/device-control/infrastructure/api/valve-api.service.js"],

    // Monitoring pages
    ["src/app/monitoring/presentation/pages/reports", "src/app/monitoring/presentation/pages/report"],
    ["src/app/monitoring/presentation/pages/report/reports-page.component.vue", "src/app/monitoring/presentation/pages/report/report-page.component.vue"],
];

const replacements = [
    // App routes imports
    ["./plans/presentation/routes/plans.routes", "./plan/presentation/routes/plan.routes"],
    ["./notifications/presentation/routes/notifications.routes", "./notification/presentation/routes/notification.routes"],

    // Plans identifiers
    ["plansRoutes", "planRoutes"],
    ["PlansPage", "PlanPage"],
    ["PlansStore", "PlanStore"],
    ["usePlansStore", "usePlanStore"],
    ["plansFacade", "planFacade"],
    ["PlansFacade", "PlanFacade"],
    ["plansApiService", "planApiService"],
    ["PlansApiService", "PlanApiService"],
    ["PlansApiEndpoint", "PlanApiEndpoint"],

    // Plans import paths
    ["../pages/plans/plans-page.component.vue", "../pages/plan/plan-page.component.vue"],
    ["../../application/store/plans.store", "../../application/store/plan.store"],
    ["../services/plans.facade", "../services/plan.facade"],
    ["../../infrastructure/api/plans-api.service", "../../infrastructure/api/plan-api.service"],
    ["../../infrastructure/api/plans-api.endpoint", "../../infrastructure/api/plan-api.endpoint"],

    // Notifications identifiers
    ["notificationsRoutes", "notificationRoutes"],
    ["AlertsPage", "AlertPage"],
    ["NotificationsStore", "NotificationStore"],
    ["useNotificationsStore", "useNotificationStore"],
    ["notificationsFacade", "notificationFacade"],
    ["NotificationsFacade", "NotificationFacade"],
    ["notificationsApiService", "notificationApiService"],
    ["NotificationsApiService", "NotificationApiService"],
    ["AlertsApiEndpoint", "AlertApiEndpoint"],

    // Notifications import paths
    ["../pages/alerts/alerts-page.component.vue", "../pages/alert/alert-page.component.vue"],
    ["../../application/store/notifications.store", "../../application/store/notification.store"],
    ["../services/notifications.facade", "../services/notification.facade"],
    ["../../infrastructure/api/notifications-api.service", "../../infrastructure/api/notification-api.service"],
    ["../../infrastructure/api/alerts-api.endpoint", "../../infrastructure/api/alert-api.endpoint"],

    // Workplace page identifiers
    ["SitesPage", "SitePage"],
    ["../pages/sites/sites-page.component.vue", "../pages/site/site-page.component.vue"],
    ["SitesApiService", "SiteApiService"],
    ["sitesApiService", "siteApiService"],
    ["SitesApiEndpoint", "SiteApiEndpoint"],
    ["../../infrastructure/api/sites-api.service", "../../infrastructure/api/site-api.service"],
    ["../../infrastructure/api/sites-api.endpoint", "../../infrastructure/api/site-api.endpoint"],

    // Device page identifiers
    ["DevicesPage", "DevicePage"],
    ["../pages/devices/devices-page.component.vue", "../pages/device/device-page.component.vue"],
    ["DevicesApiService", "DeviceApiService"],
    ["devicesApiService", "deviceApiService"],
    ["DevicesApiEndpoint", "DeviceApiEndpoint"],
    ["SensorsApiService", "SensorApiService"],
    ["sensorsApiService", "sensorApiService"],
    ["ValvesApiService", "ValveApiService"],
    ["valvesApiService", "valveApiService"],
    ["../../infrastructure/api/devices-api.service", "../../infrastructure/api/device-api.service"],
    ["../../infrastructure/api/devices-api.endpoint", "../../infrastructure/api/device-api.endpoint"],
    ["../../infrastructure/api/sensors-api.service", "../../infrastructure/api/sensor-api.service"],
    ["../../infrastructure/api/valves-api.service", "../../infrastructure/api/valve-api.service"],

    // Monitoring page identifiers
    ["ReportsPage", "ReportPage"],
    ["../pages/reports/reports-page.component.vue", "../pages/report/report-page.component.vue"],
];

function exists(relativePath) {
    return fs.existsSync(path.join(root, relativePath));
}

function renamePath(from, to) {
    const fromPath = path.join(root, from);
    const toPath = path.join(root, to);

    if (!fs.existsSync(fromPath)) {
        return;
    }

    fs.mkdirSync(path.dirname(toPath), { recursive: true });

    if (fs.existsSync(toPath)) {
        console.log(`SKIP: ${to} already exists`);
        return;
    }

    try {
        fs.renameSync(fromPath, toPath);
        console.log(`RENAMED: ${from} -> ${to}`);
    } catch (error) {
        console.log(`RENAME FAILED: ${from} -> ${to}`);
        console.log(`Reason: ${error.code}`);
        console.log("Trying copy + remove fallback...");

        fs.cpSync(fromPath, toPath, {
            recursive: true,
            force: true,
        });

        fs.rmSync(fromPath, {
            recursive: true,
            force: true,
        });

        console.log(`COPIED AND REMOVED: ${from} -> ${to}`);
    }
}

function walk(directory, files = []) {
    if (!fs.existsSync(directory)) {
        return files;
    }

    const entries = fs.readdirSync(directory, { withFileTypes: true });

    for (const entry of entries) {
        if (excludedDirs.has(entry.name)) {
            continue;
        }

        const fullPath = path.join(directory, entry.name);

        if (entry.isDirectory()) {
            walk(fullPath, files);
            continue;
        }

        if (entry.isFile() && codeExtensions.has(path.extname(entry.name))) {
            files.push(fullPath);
        }
    }

    return files;
}

function replaceInFile(filePath) {
    let content = fs.readFileSync(filePath, "utf8");
    let updated = content;

    for (const [from, to] of replacements) {
        updated = updated.split(from).join(to);
    }

    if (updated !== content) {
        fs.writeFileSync(filePath, updated, "utf8");
        console.log(`UPDATED: ${path.relative(root, filePath)}`);
    }
}

for (const [from, to] of pathRenames) {
    renamePath(from, to);
}

const files = [
    ...walk(path.join(root, "src")),
];

for (const file of files) {
    replaceInFile(file);
}

console.log("\nDone. Now run: npm run dev");