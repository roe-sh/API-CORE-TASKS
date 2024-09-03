// Define specific URLs for API requests
const getCategoriesUrl = "https://localhost:7277/api/categories/getall";
const addCategoryUrl = "https://localhost:7277/api/categories/add";

// Function to fetch categories
async function fetchCategories() {
    try {
        // Fetch categories from the server
        const response = await fetch(getCategoriesUrl, {
            method: 'GET'
        });

    
        const data = await response.json();

    
        const categoryTableBody = document.getElementById('categoryform');
        categoryTableBody.innerHTML = ''; 

        
        data.forEach(category => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${category.categoryId}</td>
                <td>${category.categoryName}</td>
                <td>
                    <button class="btn btn-sm btn-warning" onclick="editCategory(${category.categoryId}, '${category.categoryName}')">Edit</button>
                    <button class="btn btn-sm btn-danger" onclick="deleteCategory(${category.categoryId})">Delete</button>
                </td>
            `;
            categoryTableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error fetching categories:', error);
        alert('Failed to fetch categories. Please try again.');
    }
}

// Function to add a category
async function addCategory(event) {
    event.preventDefault();  // Prevent default form submission

    const form = document.getElementById('categoryForm');
    const formData = new FormData(form);  // Collect form data

    try {

        const response = await fetch(addCategoryUrl, {
            method: 'POST',
            body: formData
        });

        // Check if the response is OK
        if (response.ok) {
            alert("New category added successfully");
            $('categoriesDTO').modal('hide'); // Hide the modal
            fetchCategories();  // Refresh the category list
        } else {
            alert("Failed to add category. Please try again.");
        }
    } catch (error) {
        console.error('Error:', error);
        alert("An error occurred. Please try again.");
    }
}











// const url="https://localhost:7277/api/Categories/api/categories/getall"

// var form = document.getElementById('form');
// async function addCategory() {
//     debugger
//     event.preventDefault();
//     const formData = new FormData(form);   

// var respone = await fetch(url, {
//     method : 'POST',
//     body : formData
// })
//     alert("new category added successfully");

// }


// // function fetchCategories() {
// //     fetch(`${apiBaseUrl}/category`)
// //         .then(response => response.json())
// //         .then(data => {
// //             const categoryTableBody = document.getElementById('categoryTableBody');
// //             categoryTableBody.innerHTML = '';
// //             data.forEach(category => {
// //                 const row = document.createElement('tr');
// //                 row.innerHTML = `
// //                     <td>${category.categoryId}</td>
// //                     <td>${category.categoryName}</td>
// //                     <td>
// //                         <button class="btn btn-sm btn-warning" onclick="editCategory(${category.categoryId}, '${category.categoryName}')">Edit</button>
// //                         <button class="btn btn-sm btn-danger" onclick="deleteCategory(${category.categoryId})">Delete</button>
// //                     </td>
// //                 `;
// //                 categoryTableBody.appendChild(row);
// //             });
// //         });
// // }

// // Fetch and display products in table
// function fetchProducts() {
//     fetch(`${apiBaseUrl}/product`)
//         .then(response => response.json())
//         .then(data => {
//             const productTableBody = document.getElementById('productTableBody');
//             productTableBody.innerHTML = '';
//             data.forEach(product => {
//                 const row = document.createElement('tr');
//                 row.innerHTML = `
//                     <td>${product.productId}</td>
//                     <td>${product.productName}</td>
//                     <td>${product.description}</td>
//                     <td>$${product.price.toFixed(2)}</td>
//                     <td>${product.categoryId}</td>
//                     <td>
//                         <button class="btn btn-sm btn-warning" onclick="editProduct(${product.productId}, '${product.productName}', '${product.description}', ${product.price}, ${product.categoryId})">Edit</button>
//                         <button class="btn btn-sm btn-danger" onclick="deleteProduct(${product.productId})">Delete</button>
//                     </td>
//                 `;
//                 productTableBody.appendChild(row);
//             });
//         });
// }



// function saveCategory() {
//     const id = document.getElementById('categoryId').value;
//     const name = document.getElementById('categoryName').value;

//     if (id) {
//         fetch(`${apiBaseUrl}/category/${id}`, {
//             method: 'PUT',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({ id, name })
//         }).then(() => {
//             $('#categoryModal').modal('hide');
//             fetchCategories();
//         });
//     } else {
//         fetch(`${apiBaseUrl}/category`, {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({ name })
//         }).then(() => {
//             $('#categoryModal').modal('hide');
//             fetchCategories();
//         });
//     }
// }

// // Save Product
// function saveProduct() {
//     const id = document.getElementById('productId').value;
//     const name = document.getElementById('productName').value;
//     const description = document.getElementById('productDescription').value;
//     const price = parseFloat(document.getElementById('productPrice').value);
//     const categoryId = parseInt(document.getElementById('productCategory').value);

//     if (id) {
//         fetch(`${apiBaseUrl}/product/${id}`, {
//             method: 'PUT',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({ id, name, description, price, categoryId })
//         }).then(() => {
//             $('#productModal').modal('hide');
//             fetchProducts();
//         });
//     } else {
//         fetch(`${apiBaseUrl}/product`, {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({ name, description, price, categoryId })
//         }).then(() => {
//             $('#productModal').modal('hide');
//             fetchProducts();
//         });
//     }
// }

// // Edit Category
// function editCategory(id, name) {
//     document.getElementById('categoryId').value = id;
//     document.getElementById('categoryName').value = name;
//     $('#categoryModal').modal('show');
// }

// // Edit Product
// function editProduct(id, name, description, price, categoryId) {
//     document.getElementById('productId').value = id;
//     document.getElementById('productName').value = name;
//     document.getElementById('productDescription').value = description;
//     document.getElementById('productPrice').value = price;
//     document.getElementById('productCategory').value = categoryId;
//     $('#productModal').modal('show');
// }

// // Delete Category
// function deleteCategory(id) {
//     fetch(`${apiBaseUrl}/category/${id}`, {
//         method: 'DELETE'
//     }).then(() => {
//         fetchCategories();
//     });
// }

// // Delete Product
// function deleteProduct(id) {
//     fetch(`${apiBaseUrl}/product/${id}`, {
//         method: 'DELETE'
//     }).then(() => {
//         fetchProducts();
//     });
// }

// // Fetch Categories for Product Modal Dropdown
// function fetchCategoriesForProductModal() {
//     fetch(`${apiBaseUrl}/category`)
//         .then(response => response.json())
//         .then(data => {
//             const productCategorySelect = document.getElementById('productCategory');
//             productCategorySelect.innerHTML = '';
//             data.forEach(category => {
//                 const option = document.createElement('option');
//                 option.value = category.id;
//                 option.textContent = category.name;
//                 productCategorySelect.appendChild(option);
//             });
//         });
// }


// fetchCategories();
// fetchProducts();
