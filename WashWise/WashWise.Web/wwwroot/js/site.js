document.addEventListener("DOMContentLoaded", function () {
    const deleteModal = document.getElementById('deleteModal');
    if (!deleteModal) return;

    deleteModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const itemName = button.getAttribute('data-item-name') || 'този елемент';
        const deleteUrl = button.getAttribute('data-delete-url');

        const modalItemName = deleteModal.querySelector('#deleteItemName');
        const form = deleteModal.querySelector('#deleteForm');

        modalItemName.textContent = itemName;
        form.action = deleteUrl;
    });
});