export function attachCircularRing(svg, dotnetRef) {
    let dragging = false;
    function calculatePercentage(clientX, clientY) {
        const rect = svg.getBoundingClientRect();

        const cx = rect.left + rect.width / 2;
        const cy = rect.top + rect.height / 2;

        const dx = clientX - cx;
        const dy = clientY - cy;

        const distance = Math.sqrt(dx * dx + dy * dy);

        // El anillo visual en el SVG equivale aprox a radio 58 sobre canvas 170
        const radius = (58 / 170) * rect.width;
        const minDistance = radius - 18;
        const maxDistance = radius + 22;

        // solo reaccionar cerca del anillo
        if (distance < minDistance || distance > maxDistance) {
            return null;
        }

        // ángulo en grados
        let angle = Math.atan2(dy, dx) * 180 / Math.PI;

        // convertir a sistema:
        // arriba = 0%
        // derecha = 25%
        // abajo = 50%
        // izquierda = 75%
        angle = (angle + 90 + 360) % 360;

        const percent = Math.round((angle / 360) * 100);
        return Math.max(0, Math.min(100, percent));
    }

    async function pushValue(event) {
        const value = calculatePercentage(event.clientX, event.clientY);
        if (value !== null) {
            await dotnetRef.invokeMethodAsync("UpdateFromJs", value);
        }
    }

    function onPointerDown(event) {
        dragging = true;
        svg.setPointerCapture?.(event.pointerId);
        event.preventDefault();
        pushValue(event);
    }

    function onPointerMove(event) {
        if (!dragging) return;
        pushValue(event);
    }

    function onPointerUp(event) {
        dragging = false;
        try {
            svg.releasePointerCapture?.(event.pointerId);
        } catch { }
    }

    svg.addEventListener("pointerdown", onPointerDown);
    window.addEventListener("pointermove", onPointerMove);
    window.addEventListener("pointerup", onPointerUp);
    window.addEventListener("pointercancel", onPointerUp);

    return {
        dispose: () => {
            svg.removeEventListener("pointerdown", onPointerDown);
            window.removeEventListener("pointermove", onPointerMove);
            window.removeEventListener("pointerup", onPointerUp);
            window.removeEventListener("pointercancel", onPointerUp);
        }
    };
}