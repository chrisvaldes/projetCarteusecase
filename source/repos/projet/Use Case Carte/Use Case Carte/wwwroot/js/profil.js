function initProfileModals() {
    const modal = document.getElementById("createProfileModal");
    const openBtn = document.getElementById("openProfileModalBtn");
    if (openBtn && modal) {
        openBtn.addEventListener('click', () => {
            if (window.bootstrap && bootstrap.Modal) {
                bootstrap.Modal.getOrCreateInstance(modal).show();
            } else {
                modal.style.display = 'block';
                modal.classList.add('show');
                if (!document.querySelector('.modal-backdrop')) {
                    const backdrop = document.createElement('div');
                    backdrop.className = 'modal-backdrop fade show';
                    document.body.appendChild(backdrop);
                }
            }
        });
    }
}
window.initProfileModals = initProfileModals;

function hideCreateProfileModal() {
    const modal = document.getElementById("createProfileModal");
    if (!modal) return;
    try {
        if (window.hideModal) {
            hideModal(modal);
            return;
        }
        modal.style.display = 'none';
        modal.classList.remove('show');
        modal.setAttribute('aria-hidden', 'true');
        // remove generated backdrop if any
        document.querySelectorAll('div.modal-backdrop[data-generated="true"]').forEach(el => el.remove());
    } catch (ex) {
        console.warn('hideCreateProfileModal failed', ex);
    }
}
window.hideCreateProfileModal = hideCreateProfileModal;