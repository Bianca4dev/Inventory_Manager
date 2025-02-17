const apiUrl = "http://localhost:5230/api/items";

async function fetchItems() {
    const response = await fetch(apiUrl);
    const items = await response.json();
    console.log(items);

    const itemsList = document.getElementById("items-list");
    itemsList.innerHTML = '';  // Clear existing list before rendering

    items.forEach(item => {
        const li = document.createElement('li');
        li.textContent = `${item.id} - Quantity: ${item.quantity} - Price: R${item.price.toFixed(2)}`;
        itemsList.appendChild(li);

        // Add delete button for each item
        const deleteButton = document.createElement('button');
        deleteButton.textContent = 'Delete';
        deleteButton.onclick = () => deleteItem(item.id); // Pass item id to deleteItem
        li.appendChild(deleteButton);
    });
}

async function addItem() {
    const name = document.getElementById("item-name").value;
    const quantity = document.getElementById("item-quantity").value;
    const price = document.getElementById("item-price").value;
    const item = { name, quantity: parseInt(quantity), price: parseFloat(price) };

    // Send POST request to add new item
    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    });

    // After the item is added, fetch and display updated list
    if (response.ok) {
        fetchItems();  // Reload items after adding a new one
    } else {
        console.error('Failed to add item');
    }
}

async function deleteItem(id) {
    // Send DELETE request to remove item by id
    await fetch(`${apiUrl}/${id}`,{
        method:'DELETE'
    }
    );
    fetchItems();
    
}

const sidebar = document.getElementById('sidebar');
const toggleBtn = document.getElementById('toggleBtn');
const indicator = document.getElementById('indicator');
const menuItems = document.querySelectorAll('.sidebar ul li');

// Toggle sidebar open/close
toggleBtn.addEventListener('click', () => {
    sidebar.classList.toggle('open'); // This will toggle the 'open' class correctly
    // Adjust button icon based on sidebar state
    if (sidebar.classList.contains('open')) {
        toggleBtn.innerHTML = '<i class="fa-solid fa-chevron-left"></i>';
    } else {
        toggleBtn.innerHTML = '<i class="fa-solid fa-chevron-right"></i>';
    }
});

// Move the indicator line on hover
menuItems.forEach((item, index) => {
    item.addEventListener('mouseover', () => {
        const itemHeight = item.offsetHeight;
        const offsetTop = item.offsetTop;
        indicator.style.top = `${offsetTop}px`;
        indicator.style.height = `${itemHeight}px`; // Match the height of the current item
    });
});

// Load the items when the page is loaded
window.onload = fetchItems;
