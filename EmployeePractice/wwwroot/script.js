const apiBaseUrl = 'https://localhost:7042/api/Employee'; // Replace with your API base URL

async function fetchEmployees() {
    try {
        const response = await fetch(apiBaseUrl);
        const employees = await response.json();
        const tableBody = document.getElementById('employee-table-body');
        tableBody.innerHTML = '';

        employees.forEach(employee => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${employee.employeeNo}</td>
                <td>${employee.firstName}</td>
                <td>${employee.lastName}</td>
                <td>${new Date(employee.dateOfBirth).toLocaleDateString()}</td>
                <td>$${employee.salary.toFixed(2)}</td>
                <td>
                    <button onclick="openEditModal(${employee.employeeNo})">Edit</button>
                    <button onclick="confirmDelete(${employee.employeeNo})">Delete</button>
                </td>
            `;
            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error fetching employees:', error);
    }
}

async function handleFormSubmit(event) {
    event.preventDefault();

    const id = document.getElementById('employee-id').value;
    const firstName = document.getElementById('first-name').value;
    const lastName = document.getElementById('last-name').value;
    const dateOfBirth = document.getElementById('date-of-birth').value;
    const salary = document.getElementById('salary').value;

    const employee = { firstName, lastName, dateOfBirth, salary };

    try {
        if (id) {
            // Update existing employee
            await fetch(`${apiBaseUrl}/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ employeeNo: id, ...employee })
            });
        } else {
            // Add new employee
            await fetch(apiBaseUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(employee)
            });
        }
        fetchEmployees(); // Refresh the employee list
        clearForm();
        closeAddModal();
    } catch (error) {
        console.error('Error saving employee:', error);
    }
}

async function deleteEmployee(employeeNo) {
    try {
        await fetch(`${apiBaseUrl}/${employeeNo}`, { method: 'DELETE' });
        fetchEmployees(); // Refresh the employee list
    } catch (error) {
        console.error('Error deleting employee:', error);
    }
}

async function getAverageSalary() {
    try {
        const response = await fetch(`${apiBaseUrl}/average-salary`);
        const averageSalary = await response.json();
        document.getElementById('average-salary').textContent = `Average Salary: $${averageSalary}`;
    } catch (error) {
        console.error('Error fetching average salary:', error);
    }
}

function clearForm() {
    document.getElementById('employee-id').value = '';
    document.getElementById('first-name').value = '';
    document.getElementById('last-name').value = '';
    document.getElementById('date-of-birth').value = '';
    document.getElementById('salary').value = '';
}

function openAddEmployeeModal() {
    clearForm(); // Clear form fields
    document.getElementById('add-modal').style.display = 'block'; // Show the modal
}

function closeAddModal() {
    document.getElementById('add-modal').style.display = 'none'; // Hide the modal
}

function openEditModal(id) {
    // Fetch the employee details and populate the form in the modal
    fetch(`${apiBaseUrl}/${id}`)
        .then(response => response.json())
        .then(employee => {
            document.getElementById('edit-employee-id').value = employee.employeeNo;
            document.getElementById('edit-first-name').value = employee.firstName;
            document.getElementById('edit-last-name').value = employee.lastName;
            document.getElementById('edit-date-of-birth').value = new Date(employee.dateOfBirth).toISOString().split('T')[0];
            document.getElementById('edit-salary').value = employee.salary;
            document.getElementById('edit-modal').style.display = 'block'; // Show the modal
        })
        .catch(error => console.error('Error fetching employee details:', error));
}

function closeEditModal() {
    document.getElementById('edit-modal').style.display = 'none'; // Hide the modal
}

function confirmDelete(employeeNo) {
    const confirmed = window.confirm('Are you sure you want to delete this employee?');
    if (confirmed) {
        deleteEmployee(employeeNo);
    }
}

async function updateEmployee(event) {
    event.preventDefault();

    const id = document.getElementById('edit-employee-id').value;
    const firstName = document.getElementById('edit-first-name').value;
    const lastName = document.getElementById('edit-last-name').value;
    const dateOfBirth = document.getElementById('edit-date-of-birth').value;
    const salary = document.getElementById('edit-salary').value;

    const employee = { employeeNo: id, firstName, lastName, dateOfBirth, salary };

    try {
        await fetch(`${apiBaseUrl}/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(employee)
        });
        fetchEmployees(); // Refresh the employee list
        closeEditModal();
    } catch (error) {
        console.error('Error updating employee:', error);
    }
}

// Initial fetch to populate employee list
fetchEmployees();