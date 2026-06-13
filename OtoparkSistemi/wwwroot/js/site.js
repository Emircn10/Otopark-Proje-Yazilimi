// Active nav link highlight
document.addEventListener('DOMContentLoaded', () => {
    const path = window.location.pathname.toLowerCase();
    document.querySelectorAll('.nav-link').forEach(link => {
        const href = link.getAttribute('href')?.toLowerCase();
        if (!href) return;
        if (href === '/' && path === '/') { link.classList.add('active'); return; }
        if (href !== '/' && path.startsWith(href)) link.classList.add('active');
    });

    // Live clock
    const clockEl = document.getElementById('live-clock');
    if (clockEl) {
        const tick = () => {
            const now = new Date();
            clockEl.textContent = now.toLocaleString('tr-TR', { dateStyle: 'medium', timeStyle: 'short' });
        };
        tick();
        setInterval(tick, 1000);
    }

    // Auto-uppercase plaka inputs
    document.querySelectorAll('.plaka-input').forEach(el => {
        el.addEventListener('input', () => el.value = el.value.toUpperCase());
    });

    // Auto-dismiss alerts
    document.querySelectorAll('.alert-auto-dismiss').forEach(el => {
        setTimeout(() => el.style.opacity = '0', 3500);
        setTimeout(() => el.remove(), 4000);
    });
});
