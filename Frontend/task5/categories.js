/** @format */

async function getAllCategory() {
  let url = "https://localhost:7277/api/Categories/api/categories/getall";

  let request = await fetch(url);

  let data = await request.json();
  let cardContainer = document.getElementById("table");

  console.log(data);

  data.forEach((category) => {
    cardContainer.innerHTML += `
   <tr>
        <td>${category.categoryName}</td>
       
        <td>
          <img
            src="/BackEnd/Task5/Images/${category.categoryImage}"
            width="80"
            height="80"
            alt="Alternate Text"
          />
        </td>
        <td>
          <a href="/FrontEnd/Category/EditCategory.html" onclick = setLocalStorage(${category.categoryId})>Edit</a>
        </td>
    </tr>
      `;
  });
}

function setLocalStorage(id) {
  localStorage.categoryId = id;
}

getAllCategory();

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

const formRef = document.getElementById("addCategoryForm");
console.log(formRef);
formRef.addEventListener("submit", (event) => {
  event.preventDefault();

  const formData = new FormData(formRef);
  console.log(formData);
  console.log(formData.get("CategoryName"));

  fetch("https://localhost:7277/api/Categories/AddCategory", {
    method: "POST",
    body: formData,
  });

  alert("Category was added successfully");
});