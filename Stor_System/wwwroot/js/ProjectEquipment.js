// *************************************************** Function to filter products based on selected criteria ************************************************************
$(document).ready(function () {
    // Function to filter products based on selected criteria
    function filterProducts() {
        var selectedCategory = $('#CategoryId').val();
        var selectedBrand = $('#BrandId').val();
        var selectedInventory = $('#InventoryId').val();
        var searchName = $('.F_PH').val().toLowerCase();

        console.log(selectedCategory + selectedBrand + selectedInventory)

        $('.product-card-container').each(function () {
            var productCategory = $(this).data('category');
            var productBrand = $(this).data('brand');
            var productInventory = $(this).data('inventory');
            var productName = $(this).find('.product-title h3').text().toLowerCase();

            var categoryMatch = (selectedCategory === "" || productCategory == selectedCategory);
            var brandMatch = (selectedBrand === "" || productBrand == selectedBrand);
            var inventoryMatch = (selectedInventory === "" || productInventory == selectedInventory);
            var nameMatch = (searchName === "" || productName.includes(searchName));

            if (categoryMatch && brandMatch && inventoryMatch && nameMatch) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    }

    // Event listeners for the dropdowns and search box
    $('#CategoryId, #BrandId, #InventoryId').change(function () {
        filterProducts();
    });

    $('.F_PH').keyup(function () {
        filterProducts();
    });

    // Initial filtering on page load
    filterProducts();
});

// *************************************************** Project Main Function (Equipment collection , Mendetary Method ) ************************************************************
document.addEventListener("DOMContentLoaded", () => {
    const products = document.querySelectorAll(".product-card-container");

    products.forEach(product => {
        const productId = product.querySelector("input[type='number']").id.split('-')[2];
        const quantityInput = document.getElementById(`quantity-input-${productId}`);
        const increaseButton = document.getElementById(`increase-${productId}`);
        const decreaseButton = document.getElementById(`decrease-${productId}`);
        const maxQuantity = parseInt(quantityInput.max);

        const minValue = 0;

        increaseButton.addEventListener("click", () => {
            let currentValue = parseInt(quantityInput.value);
            if (currentValue < maxQuantity) {
                quantityInput.value = currentValue + 1;
                document.getElementById(`quantity-${productId}`).innerText = quantityInput.value;
            }
        });

        decreaseButton.addEventListener("click", () => {
            let currentValue = parseInt(quantityInput.value);
            if (currentValue > minValue) {
                quantityInput.value = currentValue - 1;
                document.getElementById(`quantity-${productId}`).innerText = quantityInput.value;
            }
        });
    });

    document.getElementById("logProductIds").addEventListener("click", () => {
        const nonZeroProductIds = [];
        const nonZeroQuantities = [];
        products.forEach(product => {
            const productId = product.querySelector("input[type='number']").id.split('-')[2];
            const quantityInput = document.getElementById(`quantity-input-${productId}`);
            const quantity = parseInt(quantityInput.value);
            if (quantity !== 0) {
                nonZeroProductIds.push(productId);
                nonZeroQuantities.push(quantity);
            }
        });

        document.getElementById("productIds").value = nonZeroProductIds;
        document.getElementById("quantities").value = nonZeroQuantities;

        console.log('One');
        document.forms["Form1"].submit();
    });
});














//function test() {
//    const products = document.querySelectorAll(".product-card-container");

//    products.forEach(product => {
//        const productId = product.querySelector("input[type='number']").id.split('-')[2];
//        const quantityInput = document.getElementById(`quantity-input-${productId}`);
//        const increaseButton = document.getElementById(`increase-${productId}`);
//        const decreaseButton = document.getElementById(`decrease-${productId}`);
//        const maxQuantity = parseInt(quantityInput.max);

//        const minValue = 0;

//        increaseButton.addEventListener("click", () => {
//            let currentValue = parseInt(quantityInput.value);
//            if (currentValue < maxQuantity) {
//                quantityInput.value = currentValue + 1;
//                document.getElementById(`quantity-${productId}`).innerText = quantityInput.value;
//            }
//        });

//        decreaseButton.addEventListener("click", () => {
//            let currentValue = parseInt(quantityInput.value);
//            if (currentValue > minValue) {
//                quantityInput.value = currentValue - 1;
//                document.getElementById(`quantity-${productId}`).innerText = quantityInput.value;
//            }
//        });
//    });

//    document.getElementById("logProductIds").addEventListener("click", () => {
//        const nonZeroProductIds = [];
//        const nonZeroQuantities = [];
//        products.forEach(product => {
//            const productId = product.querySelector("input[type='number']").id.split('-')[2];
//            const quantityInput = document.getElementById(`quantity-input-${productId}`);
//            const quantity = parseInt(quantityInput.value);
//            if (quantity !== 0) {
//                nonZeroProductIds.push(productId);
//                nonZeroQuantities.push(quantity);
//            }
//        });

//        document.getElementById("productIds").value = nonZeroProductIds;
//        document.getElementById("quantities").value = nonZeroQuantities;

//    });
//    console.log('1');
//   // document.forms["Form1"].submit();
//};