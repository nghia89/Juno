var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('#frmPayment').validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email:true
                },
                phone:{
                    required: true,
                    number:true
                }
            },
            messages: {
                name: "Bạn phải nhập tên",
                address: "Bạn phải nhập địa chỉ cụ thể",
                email: {
                    required: "Bạn phải cần nhập email",
                    email: "Định dang Email chưa đúng"
                },
                phone: {
                    required: "Bạn phải nhập số điện thoại ",
                    number: "Số điện thoại phải là số"
                }
            }
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if(isNaN(quantity)==false)//nếu quantity là số mới thực hiện
            {
             
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
            }
            else
            {
                $('#amount_' + productId).text(0);
            }
            $('#TotalOder').text(numeral(cart.getToTalOder()).format('0,0'));

            cart.updateAll();
        });
        $('#btnContinue').off('click').on('click',function(e){
            e.preventDefault();
            window.location.href="/";
        });

        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });
        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });

        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isvalid = $('#frmPayment').valid();
            if (isvalid) {
                cart.createOrder();
            }
           
        });
        
    },
    getToTalOder:function(){
        var listextBox = $('.txtQuantity');
        var total = 0;
        $.each(listextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'))
        });
        return total;
    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },

    updateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    console.log('Update ok');
                }
            }
        });
    },
    //deleteAllItem: function () {
    //    $.ajax({
    //        url: '/ShoppingCart/DeleteAll',
    //        type: 'POST',
    //        dataType: 'json',
    //        success: function (response) {
    //            cart.loadData();
    //        }

    //    })
    //},
    addItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    alert('Thêm sản phẫm thành công.');
                }
            }

        })
    },

    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            //PaymentMethod: $('input[name="paymentMethod"]:checked').val(),
            //BankCode: $('input[groupname="bankcode"]:checked').prop('id'),
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    if (response.urlCheckout != undefined && response.urlCheckout != '') {

                        window.location.href = response.urlCheckout;
                    }
                    else {
                        console.log('create order ok');
                        $('#divCheckout').hide();
                        cart.deleteAll();
                        setTimeout(function () {
                            $('#cartContent').html(' <div class="in-down alert alert-success"><strong>Cảm ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ sớm nhất.</strong></div> ');
                        }, 2000);
                    }

                }
                else {
                    $('#divMessage').show();
                    $('#divMessage').text(response.message);
                }
            }
        });
    },

    deleteItem:function(productId){
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId:productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if(response.status)
                {
                    cart.loadData();
                }
            }
        });
    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'get',
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            Name: item.Product.Name,
                            DefaultImage: item.Product.DefaultImage,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            //PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                            //Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });       
                    });
                   
                    $('#CartBody').html(html);
                    if (html == '') {
                        $('#cartContent').html('Hiện tại không có sản phẫm nào trong giỏ hàng.')
                    }
                    $('#TotalOder').text(numeral(cart.getToTalOder()).format('0,0'));
                    cart.registerEvent();// sau khi loading data xong thì gọi lại /// deleteItem thì gọi lại 
                }
            }
        });
    }
}
cart.init();