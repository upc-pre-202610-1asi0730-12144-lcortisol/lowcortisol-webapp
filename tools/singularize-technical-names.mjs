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

const folderRenames = [
    ["src/app/iam/application/dtos", "src/app/iam/application/dto"],
    ["src/app/iam/infrastructure/resources", "src/app/iam/infrastructure/resource"],
    ["src/app/iam/infrastructure/assemblers", "src/app/iam/infrastructure/assembler"],

    ["src/app/plan/application/dtos", "src/app/plan/application/dto"],
    ["src/app/plan/infrastructure/resources", "src/app/plan/infrastructure/resource"],
    ["src/app/plan/infrastructure/assemblers", "src/app/plan/infrastructure/assembler"],

    ["src/app/workplace/application/dtos", "src/app/workplace/application/dto"],
    ["src/app/workplace/infrastructure/resources", "src/app/workplace/infrastructure/resource"],
    ["src/app/workplace/infrastructure/assemblers", "src/app/workplace/infrastructure/assembler"],

    ["src/app/device-control/application/dtos", "src/app/device-control/application/dto"],
    ["src/app/device-control/infrastructure/resources", "src/app/device-control/infrastructure/resource"],
    ["src/app/device-control/infrastructure/assemblers", "src/app/device-control/infrastructure/assembler"],

    ["src/app/monitoring/application/dtos", "src/app/monitoring/application/dto"],
    ["src/app/monitoring/infrastructure/resources", "src/app/monitoring/infrastructure/resource"],
    ["src/app/monitoring/infrastructure/assemblers", "src/app/monitoring/infrastructure/assembler"],

    ["src/app/notification/application/dtos", "src/app/notification/application/dto"],
    ["src/app/notification/infrastructure/resources", "src/app/notification/infrastructure/resource"],
    ["src/app/notification/infrastructure/assemblers", "src/app/notification/infrastructure/assembler"],

    ["src/app/support/application/dtos", "src/app/support/application/dto"],
    ["src/app/support/infrastructure/resources", "src/app/support/infrastructure/resource"],
    ["src/app/support/infrastructure/assemblers", "src/app/support/infrastructure/assembler"],
];

const textReplacements = [
    // Carpetas técnicas en imports
    ["/dtos/", "/dto/"],
    ["/resources/", "/resource/"],
    ["/assemblers/", "/assembler/"],

    ["application/dtos", "application/dto"],
    ["infrastructure/resources", "infrastructure/resource"],
    ["infrastructure/assemblers", "infrastructure/assembler"],

    // Nombres de carpetas o conceptos técnicos
    ["Dtos", "Dto"],
    ["dtos", "dto"],

    ["Resources", "Resource"],
    ["resources", "resource"],

    ["Assemblers", "Assembler"],
    ["assemblers", "assembler"],

    ["Responses", "Response"],
    ["responses", "response"],

    ["Requests", "Request"],
    ["requests", "request"],
];

function renamePath(from, to) {
    const fromPath = path.join(root, from);
    const toPath = path.join(root, to);

    if (!fs.existsSync(fromPath)) {
        return;
    }

    if (fs.existsSync(toPath)) {
        console.log(`SKIP: ${to} already exists`);
        return;
    }

    fs.mkdirSync(path.dirname(toPath), { recursive: true });

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

    for (const [from, to] of textReplacements) {
        updated = updated.split(from).join(to);
    }

    if (updated !== content) {
        fs.writeFileSync(filePath, updated, "utf8");
        console.log(`UPDATED: ${path.relative(root, filePath)}`);
    }
}

function renameFilesRecursively(directory) {
    if (!fs.existsSync(directory)) {
        return;
    }

    const entries = fs.readdirSync(directory, { withFileTypes: true });

    for (const entry of entries) {
        const fullPath = path.join(directory, entry.name);

        if (entry.isDirectory()) {
            renameFilesRecursively(fullPath);
            continue;
        }

        if (!entry.isFile()) {
            continue;
        }

        let newName = entry.name;

        newName = newName.replaceAll("dtos", "dto");
        newName = newName.replaceAll("resources", "resource");
        newName = newName.replaceAll("assemblers", "assembler");
        newName = newName.replaceAll("responses", "response");
        newName = newName.replaceAll("requests", "request");

        if (newName !== entry.name) {
            const newPath = path.join(directory, newName);

            if (!fs.existsSync(newPath)) {
                try {
                    fs.renameSync(fullPath, newPath);
                    console.log(
                        `RENAMED FILE: ${path.relative(root, fullPath)} -> ${path.relative(root, newPath)}`
                    );
                } catch (error) {
                    console.log(`FAILED FILE RENAME: ${path.relative(root, fullPath)}`);
                    console.log(`Reason: ${error.code}`);
                }
            }
        }
    }
}

for (const [from, to] of folderRenames) {
    renamePath(from, to);
}

renameFilesRecursively(path.join(root, "src", "app"));

const files = walk(path.join(root, "src"));

for (const file of files) {
    replaceInFile(file);
}

console.log("\nDone. Now run: npm run dev");