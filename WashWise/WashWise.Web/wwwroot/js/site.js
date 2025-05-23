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

document.addEventListener("DOMContentLoaded", function () {
    const deleteModal = document.getElementById('roleModal');
    if (!deleteModal) return;

    deleteModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const itemName = button.getAttribute('data-item-name') || 'този елемент';
        const changeRoleUrl = button.getAttribute('data-role-url');

        const modalItemName = deleteModal.querySelector('#roleItemName');
        const form = deleteModal.querySelector('#roleForm');

        modalItemName.textContent = itemName;
        form.action = changeRoleUrl;
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("searchInput");

    if (searchInput) {
        searchInput.addEventListener("keyup", function () {
            const query = this.value.toLowerCase();
            const rows = document.querySelectorAll("#usersTable tbody tr");

            rows.forEach(row => {
                const email = row.cells[0].innerText.toLowerCase();
                row.style.display = email.includes(query) ? "" : "none";
            });
        });
    }
});