
// *************************************************** Remove Alert When Typing ************************************************************

document.getElementById('ProjectName_Input').addEventListener('input', function () { ProjectName_Alert.style.display = 'none'; });
document.getElementById('ProjectLocation_Input').addEventListener('input', function () { ProjectLocation_Alert.style.display = 'none'; });
document.getElementById('StartDate_Input').addEventListener('input', function () { StartDate_Alert.style.display = 'none'; });
document.getElementById('EndDate_Input').addEventListener('input', function () { EndDate_Alert.style.display = 'none'; });

//********************************************* Mendetary Method *********************************************

document.getElementById("logProductIds").addEventListener("click", () => {
    var ProjectName_Input = document.getElementById('ProjectName_Input').value; var ProjectName_Alert = document.getElementById('ProjectName_Alert');
    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value; var ProjectLocation_Alert = document.getElementById('ProjectLocation_Alert');
    var StartDate_Input = document.getElementById('StartDate_Input').value; var StartDate_Alert = document.getElementById('StartDate_Alert');
    var EndDate_Input = document.getElementById('EndDate_Input').value; var EndDate_Alert = document.getElementById('EndDate_Alert');

    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
        document.forms["Form1"].submit();
    }
    else {
        if (ProjectName_Input === '') { ProjectName_Alert.style.display = 'block'; }
        if (ProjectLocation_Input === '') { ProjectLocation_Alert.style.display = 'block'; }
        if (StartDate_Input === '') { StartDate_Alert.style.display = 'block'; }
        if (EndDate_Input === '') { EndDate_Alert.style.display = 'block'; }

    }
});

// ******************************************** Add The Project Name To The Notification Input ********************************************

document.getElementById('ProjectName_Input').addEventListener('input', function () {
    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
    var StartDate_Input = document.getElementById('StartDate_Input').value;
    var EndDate_Input = document.getElementById('EndDate_Input').value;

    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
    }
});

document.getElementById('ProjectLocation_Input').addEventListener('input', function () {
    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
    var StartDate_Input = document.getElementById('StartDate_Input').value;
    var EndDate_Input = document.getElementById('EndDate_Input').value;

    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
    }
});

document.getElementById('StartDate_Input').addEventListener('input', function () {
    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
    var StartDate_Input = document.getElementById('StartDate_Input').value;
    var EndDate_Input = document.getElementById('EndDate_Input').value;

    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
    }
});

document.getElementById('EndDate_Input').addEventListener('input', function () {
    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
    var StartDate_Input = document.getElementById('StartDate_Input').value;
    var EndDate_Input = document.getElementById('EndDate_Input').value;

    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
    }
});
        





























//// *************************************************** Remove Alert When Typing ************************************************************

//document.getElementById('ProjectName_Input').addEventListener('input', function () { ProjectName_Alert.style.display = 'none'; });
//document.getElementById('ProjectLocation_Input').addEventListener('input', function () { ProjectLocation_Alert.style.display = 'none'; });
//document.getElementById('StartDate_Input').addEventListener('input', function () { StartDate_Alert.style.display = 'none'; });
//document.getElementById('EndDate_Input').addEventListener('input', function () { EndDate_Alert.style.display = 'none'; });

/////* ******************************************** Add Building  taps  General Info /  Attachments *********************************************/

////document.getElementById("btn1").addEventListener("click", function () {
////    document.getElementById("div1").style.display = "block";
////    document.getElementById("btn1").style.backgroundColor = "#6c7293";
////    document.getElementById("btn1").style.color = "#FFFFFF";



////    document.getElementById("div2").style.display = "none";
////    document.getElementById("btn2").style.backgroundColor = "transparent";
////    document.getElementById("btn2").style.color = "black";
////});

////document.getElementById("btn2").addEventListener("click", function () {
////    document.getElementById("div1").style.display = "none";
////    document.getElementById("btn1").style.backgroundColor = "transparent";
////    document.getElementById("btn1").style.color = "black";


////    document.getElementById("div2").style.display = "block";
////    document.getElementById("btn2").style.backgroundColor = "#6c7293";
////    document.getElementById("btn2").style.color = "#FFFFFF";
//});


//// ******************************************** Add The Project Name To The Notification Input ********************************************

//document.getElementById('ProjectName_Input').addEventListener('input', function () {
//    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
//    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
//    var StartDate_Input = document.getElementById('StartDate_Input').value;
//    var EndDate_Input = document.getElementById('EndDate_Input').value;

//    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
//        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
//    }
//});

//document.getElementById('ProjectLocation_Input').addEventListener('input', function () {
//    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
//    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
//    var StartDate_Input = document.getElementById('StartDate_Input').value;
//    var EndDate_Input = document.getElementById('EndDate_Input').value;

//    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
//        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
//    }
//});

//document.getElementById('StartDate_Input').addEventListener('input', function () {
//    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
//    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
//    var StartDate_Input = document.getElementById('StartDate_Input').value;
//    var EndDate_Input = document.getElementById('EndDate_Input').value;

//    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
//        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
//    }
//});

//document.getElementById('EndDate_Input').addEventListener('input', function () {
//    var ProjectName_Input = document.getElementById('ProjectName_Input').value;
//    var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value;
//    var StartDate_Input = document.getElementById('StartDate_Input').value;
//    var EndDate_Input = document.getElementById('EndDate_Input').value;

//    if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {
//        document.getElementById('notificationInput').value = document.getElementById('ProjectName_Input').value;
//    }
//});


////// *************************************************** Function to filter products based on selected criteria ************************************************************
////$(document).ready(function () {
////    // Function to filter products based on selected criteria
////    function filterProducts() {
////        var selectedCategory = $('#CategoryId').val();
////        var selectedBrand = $('#BrandId').val();
////        var selectedInventory = $('#InventoryId').val();
////        var searchName = $('.F_PH').val().toLowerCase();

////        console.log(selectedCategory + selectedBrand + selectedInventory)

////        $('.product-card-container').each(function () {
////            var productCategory = $(this).data('category');
////            var productBrand = $(this).data('brand');
////            var productInventory = $(this).data('inventory');
////            var productName = $(this).find('.product-title h3').text().toLowerCase();

////            var categoryMatch = (selectedCategory === "" || productCategory == selectedCategory);
////            var brandMatch = (selectedBrand === "" || productBrand == selectedBrand);
////            var inventoryMatch = (selectedInventory === "" || productInventory == selectedInventory);
////            var nameMatch = (searchName === "" || productName.includes(searchName));

////            if (categoryMatch && brandMatch && inventoryMatch && nameMatch) {
////                $(this).show();
////            } else {
////                $(this).hide();
////            }
////        });
////    }

////    // Event listeners for the dropdowns and search box
////    $('#CategoryId, #BrandId, #InventoryId').change(function () {
////        filterProducts();
////    });

////    $('.F_PH').keyup(function () {
////        filterProducts();
////    });

////    // Initial filtering on page load
////    filterProducts();
////});




////// *************************************************** Project Main Function (Equipment collection , Mendetary Method ) ************************************************************
////document.addEventListener("DOMContentLoaded", () => {
////    const products = document.querySelectorAll(".product-card-container");

////    products.forEach(product => {
////        const productId = product.querySelector("input[type='number']").id.split('-')[2];
////        const quantityInput = document.getElementById(`quantity-input-${productId}`);
////        const increaseButton = document.getElementById(`increase-${productId}`);
////        const decreaseButton = document.getElementById(`decrease-${productId}`);
////        const maxQuantity = parseInt(quantityInput.max);

////        const minValue = 0;

////        increaseButton.addEventListener("click", () => {
////            let currentValue = parseInt(quantityInput.value);
////            if (currentValue < maxQuantity) {
////                quantityInput.value = currentValue + 1;
////                document.getElementById(`quantity-${productId}`).innerText = quantityInput.value;
////            }
////        });

////        decreaseButton.addEventListener("click", () => {
////            let currentValue = parseInt(quantityInput.value);
////            if (currentValue > minValue) {
////                quantityInput.value = currentValue - 1;
////                document.getElementById(`quantity-${productId}`).innerText = quantityInput.value;
////            }
////        });
////    });

////    document.getElementById("logProductIds").addEventListener("click", () => {
////        const nonZeroProductIds = [];
////        const nonZeroQuantities = [];
////        products.forEach(product => {
////            const productId = product.querySelector("input[type='number']").id.split('-')[2];
////            const quantityInput = document.getElementById(`quantity-input-${productId}`);
////            const quantity = parseInt(quantityInput.value);
////            if (quantity !== 0) {
////                nonZeroProductIds.push(productId);
////                nonZeroQuantities.push(quantity);
////            }
////        });

//        //********************************************* Mendetary Method *********************************************

//        var ProjectName_Input = document.getElementById('ProjectName_Input').value; var ProjectName_Alert = document.getElementById('ProjectName_Alert');
//        var ProjectLocation_Input = document.getElementById('ProjectLocation_Input').value; var ProjectLocation_Alert = document.getElementById('ProjectLocation_Alert');
//        var StartDate_Input = document.getElementById('StartDate_Input').value; var StartDate_Alert = document.getElementById('StartDate_Alert');
//        var EndDate_Input = document.getElementById('EndDate_Input').value; var EndDate_Alert = document.getElementById('EndDate_Alert');

//        if (ProjectName_Input !== '' && ProjectLocation_Input !== '' && StartDate_Input !== '' && EndDate_Input !== '') {

//            document.getElementById("productIds").value = nonZeroProductIds;
//            document.getElementById("quantities").value = nonZeroQuantities;
//            document.forms["Form1"].submit();

//        }
//        else {
//            if (ProjectName_Input === '') {
//                ProjectName_Alert.style.display = 'block';

//                //document.getElementById("div1").style.display = "block";
//                //document.getElementById("btn1").style.backgroundColor = "#6c7293";
//                //document.getElementById("btn1").style.color = "#FFFFFF";

//                //document.getElementById("div2").style.display = "none";
//                //document.getElementById("btn2").style.backgroundColor = "transparent";
//                //document.getElementById("btn2").style.color = "black";
//            }

//            if (ProjectLocation_Input === '') {
//                ProjectLocation_Alert.style.display = 'block';

//                //document.getElementById("div1").style.display = "block";
//                //document.getElementById("btn1").style.backgroundColor = "#6c7293";
//                //document.getElementById("btn1").style.color = "#FFFFFF";

//                //document.getElementById("div2").style.display = "none";
//                //document.getElementById("btn2").style.backgroundColor = "transparent";
//                //document.getElementById("btn2").style.color = "black";
//            }
//            if (StartDate_Input === '') {
//                StartDate_Alert.style.display = 'block';

//                //document.getElementById("div1").style.display = "block";
//                //document.getElementById("btn1").style.backgroundColor = "#6c7293";
//                //document.getElementById("btn1").style.color = "#FFFFFF";

//                //document.getElementById("div2").style.display = "none";
//                //document.getElementById("btn2").style.backgroundColor = "transparent";
//                //document.getElementById("btn2").style.color = "black";
//            }
//            if (EndDate_Input === '') {
//                EndDate_Alert.style.display = 'block';
//                //document.getElementById("div1").style.display = "block";
//                //document.getElementById("btn1").style.backgroundColor = "#6c7293";
//                //document.getElementById("btn1").style.color = "#FFFFFF";

//                //document.getElementById("div2").style.display = "none";
//                //document.getElementById("btn2").style.backgroundColor = "transparent";
//                //document.getElementById("btn2").style.color = "black";
//            }

//        }
//    });
//});
