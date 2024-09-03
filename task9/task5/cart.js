/** @format */

async function getAllItems() {
    let CID =1;
    let url = `https://localhost:7277/api/CartItems/${CID}`;
  
    let request = await fetch(url);
  
    let data = await request.json();
    let cardContainer = document.getElementById("table");
  
    console.log(data);
  
    data.forEach((item) => {
      cardContainer.innerHTML += `
        <tr>
            <td>${item.cartId}</td>
            <td>${item.product.productName}</td>
            <td><input id="quantity-${item.cartItemId}" type="number" value="${item.quantity}" class="form-input"></td>
            <td><button type="button" onclick="updateQuantity(${item.cartItemId})">Edit</button></td>
            <td><button type="button" onclick="deleteItem(${item.cartItemId})">Delete</button></td>
        </tr>
      `;
    });
  }
  
  getAllItems();
  
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
  async function updateQuantity(CartItemID) {
    let url = `https://localhost:7277/api/CartItems/Edit/${CartItemID}`;
  
    var quantityInput = document.getElementById(`quantity-${CartItemID}`);
    var quantity = quantityInput.value;
  
    let response = await fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({Quantity:quantity}),
    });
  
    location.reload();
  }
  
  ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
  async function deleteItem(CartItemID) {
    let url = `https://localhost:7277/api/CartItems/Delete/${CartItemID}`;
  
    fetch(url, {
      method: "DELETE",
    });
  
    location.reload();
  }