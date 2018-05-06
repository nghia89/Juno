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
    addItem:function(productId){
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId:productId
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
                    $('#TotalOder').text(numeral(cart.getToTalOder()).format('0,0'));
                    cart.registerEvent();// sau khi loading data xong thì gọi lại /// deleteItem thì gọi lại 
                }
            }
        });
    }
}
cart.init();