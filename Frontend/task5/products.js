/** @format */

var id = localStorage.getItem("categoryId");

var url = `https://localhost:7277/api/Products`;

async function getProducts() {
  
  let request = await fetch(url);

  let data = await request.json();
  let cards = document.getElementById("table");

  data.forEach((product) => {
    cards.innerHTML += `
     <tr>
        <td>${product.productName}</td>
        <td>${product.description}</td>
        <td>${product.price}</td>
        <td>
          <img
            src="/BackEnd/Task5/Images/${product.productImage}"
            width="80"
            height="80"
            alt="Alternate Text"
          />
        </td>
        <td>
          <a href="/FrontEnd/Products/EditProduct.html" onclick = setLocalStorage(${product.productId})>Edit</a>
        </td>
    </tr>
      `;
  });
}

getProducts();

function setLocalStorage(id) {
  localStorage.productId = id;
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

async function getCategoryName() {
  const dropDown = document.getElementById("dropDownList");
  let url = "https://localhost:7277/api/Categories/api/categories/getall";
  let request = await fetch(url);
  let data = await request.json();

  data.forEach((select) => {
    dropDown.innerHTML += `
    <option value="${select.categoryId}">${select.categoryName}</option>
  `;
  });
}

getCategoryName();
const formRef = document.getElementById("addProductForm");

formRef.addEventListener("submit", (event) => {
  event.preventDefault();

  const formData = new FormData(formRef);

  console.log(formData);

  fetch("https://localhost:7277/api/Products/AddProduct", {
    method: "POST",
    body: formData,
  });

  alert("Product was added successfully");
});