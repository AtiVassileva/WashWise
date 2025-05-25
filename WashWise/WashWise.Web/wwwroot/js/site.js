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
    const roleModal = document.getElementById('roleModal');
    if (!roleModal) return;

    roleModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const itemName = button.getAttribute('data-item-name') || 'този елемент';
        const changeRoleUrl = button.getAttribute('data-role-url');

        const modalItemName = roleModal.querySelector('#roleItemName');
        const form = roleModal.querySelector('#roleForm');

        modalItemName.textContent = itemName;
        form.action = changeRoleUrl;
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const cancelModal = document.getElementById('cancelModal');
    if (!cancelModal) return;

    cancelModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const itemName = button.getAttribute('data-item-name') || 'този елемент';
        const cancelUrl = button.getAttribute('data-cancel-url');

        const modalItemName = cancelModal.querySelector('#cancelItemName');
        const form = cancelModal.querySelector('#cancelForm');

        modalItemName.textContent = itemName;
        form.action = cancelUrl;
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

document.addEventListener("DOMContentLoaded", function () {
    const reservationSearchInput = document.getElementById("reservationSearch");

    if (reservationSearchInput) {
        reservationSearchInput.addEventListener("keyup",
            function() {
                var input = this.value.toLowerCase();
                var rows = document.querySelectorAll("#reservationTable tbody tr");

                rows.forEach(function(row) {
                    var userCell = row.cells[0].textContent.toLowerCase();
                    row.style.display = userCell.includes(input) ? "" : "none";
                });
            });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const reservationSearchInput = document.getElementById("buildingInput");

    if (reservationSearchInput) {
        reservationSearchInput.addEventListener("keyup",
            function () {
                var input = this.value.toLowerCase();
                var rows = document.querySelectorAll("#buildingTable tbody tr");

                rows.forEach(function (row) {
                    var userCell = row.cells[0].textContent.toLowerCase();
                    row.style.display = userCell.includes(input) ? "" : "none";
                });
            });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const reservationSearchInput = document.getElementById("machinesInput");

    if (reservationSearchInput) {
        reservationSearchInput.addEventListener("keyup",
            function () {
                var input = this.value.toLowerCase();
                var rows = document.querySelectorAll("#machinesTable tbody tr");

                rows.forEach(function (row) {
                    var userCell = row.cells[0].textContent.toLowerCase();
                    row.style.display = userCell.includes(input) ? "" : "none";
                });
            });
    }
});