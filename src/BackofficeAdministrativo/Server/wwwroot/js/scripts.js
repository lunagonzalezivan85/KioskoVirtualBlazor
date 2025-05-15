/*!
* Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
* Modified for Restaurant Management System
*/

window.addEventListener('DOMContentLoaded', event => {
    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

    // Initialize all tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize all popovers
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Function to format currency
    window.formatCurrency = (amount) => {
        return new Intl.NumberFormat('es-CR', {
            style: 'currency',
            currency: 'CRC'
        }).format(amount);
    };

    // Function to format dates
    window.formatDate = (dateString) => {
        return new Date(dateString).toLocaleDateString('es-CR', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    // Function to initialize DataTables
    window.initializeDataTable = (tableId, config = {}) => {
        const defaultConfig = {
            language: {
                lengthMenu: "Mostrar _MENU_ registros por página",
                zeroRecords: "No se encontraron registros",
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No hay registros disponibles",
                infoFiltered: "(filtrado de _MAX_ registros totales)",
                search: "Buscar:",
                paginate: {
                    first: "Primero",
                    last: "Último",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            },
            ...config
        };

        return new simpleDatatables.DataTable(document.getElementById(tableId), defaultConfig);
    };

    // Function to show toast notifications
    window.showToast = (message, type = 'success') => {
        const toastContainer = document.getElementById('toast-container');
        if (!toastContainer) {
            const container = document.createElement('div');
            container.id = 'toast-container';
            container.className = 'position-fixed bottom-0 end-0 p-3';
            document.body.appendChild(container);
        }

        const toast = document.createElement('div');
        toast.className = `toast align-items-center text-white bg-${type} border-0`;
        toast.setAttribute('role', 'alert');
        toast.setAttribute('aria-live', 'assertive');
        toast.setAttribute('aria-atomic', 'true');

        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        `;

        document.getElementById('toast-container').appendChild(toast);
        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();

        toast.addEventListener('hidden.bs.toast', () => {
            toast.remove();
        });
    };

    // Function to confirm actions
    window.confirmAction = async (message) => {
        return await Swal.fire({
            title: '¿Está seguro?',
            text: message,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, confirmar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            return result.isConfirmed;
        });
    };
});
