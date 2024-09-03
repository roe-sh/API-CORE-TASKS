/** @format */

var id = localStorage.getItem("categoryId");

async function getCategoryInfo() {
  let url = `https://localhost:44312/api/Categories/getCategoryById/${id}`;

  let request = await fetch(url);

  let data = await request.json();

  let cardContainer = document.getElementById("cardContainer");

  cardContainer.innerHTML = `
      
        <div class="card">
          <img class="card-img-top" src="/Day5-BackEnd/Day5-BackEnd/Images/${data.categoryImage}" alt="Card image cap 1" />
          <div class="card-body">
            <h5 class="card-title">${data.categoryName}</h5>
            <p class="card-text">
              ${data.categoryDescription}
            </p>
          </div>
        </div>
        `;
}

getCategoryInfo();

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

const formRef = document.getElementById("updateCategoryForm");

formRef.addEventListener("submit", (event) => {
  event.preventDefault();

  const formData = new FormData(formRef);

  fetch(`https://localhost:44333/api/Categories/EditCategory/${id}`, {
    method: "PUT",
    body: formData,
  });

  alert("Category details was uploded successfully");
});