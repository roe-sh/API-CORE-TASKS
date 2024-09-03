/** @format */

var id = localStorage.getItem("productId");
var categoryId = localStorage.getItem("categoryId");

async function getProductInfo() {
  let url = `https://localhost:44312/api/Products/getProductById/${id}`;

  let request = await fetch(url);

  let data = await request.json();

  let cardContainer = document.getElementById("cardContainer");

  cardContainer.innerHTML = `
      
    <div class="col-md mb-4">
      <div class="card">
        <img class="card-img-top" src="/Day5-BackEnd/Day5-BackEnd/Images/${data.productImage}" alt="Card image cap 1" />
        <div class="card-body">
          <h5 class="card-title">${data.productName}</h5>
          <h6> Price: ${data.price}</h6>
          <p class="card-text">
            ${data.description}
          </p>
        </div>
      </div>
    </div>
        `;
}

getProductInfo();

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

async function getCategoryName() {
  const dropDown = document.getElementById("dropDownList");
  let url = "https://localhost:44333/api/Categories/AllCategories";
  let request = await fetch(url);
  let data = await request.json();

  data.forEach((select) => {
    dropDown.innerHTML += `
    <option value="${select.categoryId}">${select.categoryName}</option>
  `;
  });
}

getCategoryName();

const formRef = document.getElementById("updateProductForm");

formRef.addEventListener("submit", (event) => {
  event.preventDefault();

  const formData = new FormData(formRef);

  fetch(`https://localhost:44333/api/Products/EditProduct/${id}`, {
    method: "PUT",
    body: formData,
  });

  alert("Product details was updated successfully");
});