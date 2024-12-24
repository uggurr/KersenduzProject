$(document).ready(function () {
  var table = $('#productTable').DataTable({
    columnDefs: [
      {
        targets: -1, // Son kolonu hedef alır
        visible: false, // Görünürlüğü gizler
        searchable: false // Arama özelliğini de devre dışı bırakır
      }
    ]
  });

 /* var unitOptions = '';*/
 // await loadProducts();
  function loadProducts() {
    $.ajax({
      url: '/api/RawMaterial/ListRawMaterialAsync', // Ürün verilerini çekeceğiniz API endpoint
      type: 'GET',
      dataType: 'json',
      success: function (data) {
        data.forEach(function (product) {
          var originalDate = new Date(product.modifietedDate);
          var formattedDate = ('0' + originalDate.getDate()).slice(-2) + '-' +
            ('0' + (originalDate.getMonth() + 1)).slice(-2) + '-' +
            originalDate.getFullYear() + ' / ' +
            ('0' + originalDate.getHours()).slice(-2) + ':' +
            ('0' + originalDate.getMinutes()).slice(-2);
          // Ürün verilerini tabloya ekle
          var unitText = product.unitName; // API'den dönen birim adı
          var rowNode = table.row.add([
            product.name,
            unitText,
            product.price,
            formattedDate,
            '<button class="btn update-btn"><i class="bi bi-pencil-square"></i></button>' +
            '<button class="btn delete-btn"><i class="bi bi-trash"></i></button>',
            product.id
          ]).draw().node();

          $(rowNode).find('td').eq(5).addClass('d-none');

          // Satırdaki butonlara olay ekle
          $(rowNode).find('.update-btn').on('click', function () {
            var row = $(this).closest('tr');
            var productName = row.find('td').eq(0).text();
            var productUnit = row.find('td').eq(1).text();
            var productPrice = row.find('td').eq(2).text();
            var productId = row.find('td').eq(5).text();

            // Satırdaki verileri input alanlarına yükle
            row.find('td').eq(0).html('<input type="text" class="form-control product-name" value="' + productName + '">');
            row.find('td').eq(1).html('<select class="form-control product-unit">' +
              '<option value="1"' + (productUnit === '1' ? ' selected' : '') + '>Kg</option>' +
              '<option value="2"' + (productUnit === '2' ? ' selected' : '') + '>Gr</option>' +
              '</select>');
            row.find('td').eq(2).html('<input type="number" class="form-control product-price" value="' + productPrice + '">');

            // Güncelleme butonunu kaydet butonuna çevir
            $(this).replaceWith('<button class="btn save-btn"><i class="bi bi-check2"></i></button>');

            // Kaydet butonuna tıklama
            row.find('.save-btn').on('click', function () {
              var updatedProductName = row.find('.product-name').val();
              var updatedProductUnit = row.find('.product-unit').val();
              var updatedProductPrice = row.find('.product-price').val();
              var updatedProductId = row.find('td').eq(5).text();

              if (updatedProductName && updatedProductPrice) {
                // Verileri güncelle
                table.cell(row, 0).data(updatedProductName).draw();
                table.cell(row, 1).data(updatedProductUnit).draw();
                table.cell(row, 2).data(updatedProductPrice).draw();

                var updatedProduct = {
                  Id: updatedProductId,
                  Name: updatedProductName,
                  Price: updatedProductPrice,
                  UnitId: updatedProductUnit
                };

                $.ajax({
                  url: '/api/RawMaterial/UpdateRawMaterialAsync',
                  type: 'POST',
                  contentType: 'application/json',
                  data: JSON.stringify(updatedProduct),
                  success: function () {
                  },
                  error: function () {
                    alert('Ürün güncellenirken bir hata oluştu.');
                  }
                });

                // Kaydet butonunu güncelleme butonuna çevir
                row.find('.save-btn').replaceWith('<button class="btn update-btn"><i class="bi bi-pencil-square"></i></button>');
              } else {
                alert('Lütfen tüm alanları doldurun.');
              }
            });
          });

          // Silme butonuna tıklama
          $(rowNode).find('.delete-btn').on('click', function () {
            var row = $(this).closest('tr');
            table.row(row).remove().draw();
          });
        });
      },
      error: function () {
        alert('Ürün verileri yüklenirken bir hata oluştu.');
      }
    });
  }

  // Sayfa yüklendiğinde ürün verilerini çek
  loadProducts();
  //await loadUnits();

  $('#addRow').on('click', function () {
    var hasUnsavedRow = false;
    var hasActiveInput = false;

    $('#productTable tbody tr').each(function () {
      if ($(this).find('.save-btn').length > 0) {
        hasUnsavedRow = true;
      }

      if ($(this).find('input, select').is(':focus')) {
        hasActiveInput = true;
      }
    });

    if (hasUnsavedRow) {
      alert('Lütfen önce mevcut satırı kaydedin.');
      return;
    }

    if (hasActiveInput) {
      alert('Lütfen önce mevcut satırı tamamlayın.');
      return;
    }

    var newRow = table.row.add([
      '<input type="text" class="form-control product-name" placeholder="Hammadde Adı">',
      '<select class="form-control product-unit">' +
      '<option value="1">Kg</option>' +
      '<option value="2">Gr</option>' +
      '</select>',
      '<input type="number" class="form-control product-price" placeholder="Fiyatı">',
      '',
      '<button class="btn save-btn"><i class="bi bi-check2"></i></button>' +
      '<button class="btn delete-btn"><i class="bi bi-trash"></i></button>',
      ''
    ]).draw().node();

    $(newRow).find('.save-btn').on('click', function () {
      var productName = $(newRow).find('.product-name').val();
      var productUnit = $(newRow).find('.product-unit').val();
      var productUnitText = $(newRow).find('.product-unit option:selected').text(); 
      var productPrice = $(newRow).find('.product-price').val();

      if (productName && productPrice) {
        var rawMaterial = {
          Name: productName,
          Price: productPrice,
          UnitId: productUnit
        }
        // AJAX ile API'ye veri gönder
        $.ajax({
          url: '/api/RawMaterial/AddRawMaterialAsync', // Backend API adresi
          type: 'POST',
          contentType: 'application/json',
          data: JSON.stringify(rawMaterial),
          success: function (response) {
            var dbProductName = response.name;
            var dbProductUnit = response.unitName;
            var dbProductPrice = response.price;
            var originalDate = new Date(response.modifietedDate);
            var formattedDate = ('0' + originalDate.getDate()).slice(-2) + '-' +
              ('0' + (originalDate.getMonth() + 1)).slice(-2) + '-' +
              originalDate.getFullYear() + ' / ' +
              ('0' + originalDate.getHours()).slice(-2) + ':' +
              ('0' + originalDate.getMinutes()).slice(-2);
            var dbId = response.id;
            // Başarılı kayıt sonrası hücreleri güncelle
            table.cell(newRow, 0).data(dbProductName).draw();
            table.cell(newRow, 1).data(dbProductUnit).draw();
            table.cell(newRow, 2).data(dbProductPrice).draw();
            table.cell(newRow, 3).data(formattedDate).draw();
            table.cell(newRow, 5).data(dbId).draw(); 

            // Kaydet butonunu güncelleme butonuna çevir
            $(newRow).find('.save-btn').replaceWith('<button class="btn update-btn"><i class="bi bi-pencil-square"></i></button>');
            $(newRow).find('.delete-btn').replaceWith('<button class="btn delete-btn"><i class="bi bi-trash"></i></button>');
          },
          error: function () {
            alert('Veri kaydedilirken bir hata oluştu!');
          }
        });
      } else {
        alert('Lütfen tüm alanları doldurun.');
      }
    });

    table.row('.newRow').draw(false);
  });

  // Güncelleme butonuna tıklama
  $('#productTable').on('click', '.update-btn', function () {
    var row = $(this).closest('tr');
    


    var productName = row.find('td').eq(0).text();
    var productUnit = row.find('td').eq(1).text();
    var productPrice = row.find('td').eq(2).text();
    // Satırdaki verileri input alanlarına yükle
    row.find('td').eq(0).html('<input type="text" class="form-control product-name" value="' + productName + '">');
    row.find('td').eq(1).html('<select class="form-control product-unit">' +
      '<option value="1"' + (productUnit === '1' ? ' selected' : '') + '>Kg</option>' +
      '<option value="2"' + (productUnit === '2' ? ' selected' : '') + '>Gr</option>' +
      '</select>');
    row.find('td').eq(2).html('<input type="number" class="form-control product-price" value="' + productPrice + '">');

    // Güncelleme butonunu kaydet butonuna çevir
    $(this).replaceWith('<button class="btn save-btn"><i class="bi bi-check2"></i></button>');

    // Kaydet butonuna tıklama
    row.find('.save-btn').on('click', function () {
      var updatedProductName = row.find('.product-name').val();
      var updatedProductUnit = row.find('.product-unit').val();
      var updatedProductPrice = row.find('.product-price').val();
      var updatedId = table.cell(row, 5).data();

      if (updatedProductName && updatedProductPrice) {
        // Verileri güncelle
        table.cell(row, 0).data(updatedProductName).draw();
        table.cell(row, 1).data(updatedProductUnit).draw();
        table.cell(row, 2).data(updatedProductPrice).draw();

        var updatedProductt = {
          Id: updatedId,
          Name: updatedProductName,
          Price: updatedProductPrice,
          UnitId: updatedProductUnit
        };

        $.ajax({
          url: '/api/RawMaterial/UpdateRawMaterialAsync',
          type: 'POST',
          contentType: 'application/json',
          data: JSON.stringify(updatedProductt),
          success: function () {
          },
          error: function () {
            alert('Ürün güncellenirken bir hata oluştu.');
          }
        });

        // Kaydet butonunu güncelleme butonuna çevir
        row.find('.save-btn').replaceWith('<button class="btn update-btn"><i class="bi bi-pencil-square"></i></button>');
      } else {
        alert('Lütfen tüm alanları doldurun.');
      }
    });
  });

  $('#productTable').on('click', '.delete-btn',function () {
    // Satırı tablodan kaldır
    var row = $(this).closest('tr');
    var table = $('#productTable').DataTable();

    // Satırı DataTable'dan kaldır
    table.row(row).remove().draw();
  });
});

//async function loadProducts() {
//  try {
//    const response = await $.ajax({
//      url: '/api/RawMaterial/ListRawMaterialAsync', // Ürün verilerini çekeceğiniz API endpoint
//      type: 'GET',
//      dataType: 'json',
//    });

//    response.forEach(function (product) {
//      // Ürün verilerini tabloya ekle
//      var unitText = product.unitName; // API'den dönen birim adı
//      var rowNode = table.row.add([
//        product.name,
//        unitText,
//        product.price,
//        '<button class="btn update-btn"><i class="bi bi-pencil-square"></i></button>' +
//        '<button class="btn delete-btn"><i class="bi bi-trash"></i></button>'
//      ]).draw().node();

//      // Satırdaki butonlara olay ekle
//      $(rowNode).find('.update-btn').on('click', function () {
//        var row = $(this).closest('tr');
//        var productName = row.find('td').eq(0).text();
//        var productUnit = row.find('td').eq(1).text();
//        var productPrice = row.find('td').eq(2).text();

//        // Satırdaki verileri input alanlarına yükle
//        row.find('td').eq(0).html('<input type="text" class="form-control product-name" value="' + productName + '">');
//        row.find('td').eq(1).html('<select class="form-control product-unit">' +
//          '<option value="Kg"' + (productUnit === 'Kg' ? ' selected' : '') + '>Kg</option>' +
//          '<option value="L"' + (productUnit === 'L' ? ' selected' : '') + '>L</option>' +
//          '<option value="Gr"' + (productUnit === 'Gr' ? ' selected' : '') + '>Gr</option>' +
//          '<option value="Mg"' + (productUnit === 'Mg' ? ' selected' : '') + '>Mg</option>' +
//          '</select>');
//        row.find('td').eq(2).html('<input type="number" class="form-control product-price" value="' + productPrice + '">');

//        // Güncelleme butonunu kaydet butonuna çevir
//        $(this).replaceWith('<button class="btn save-btn"><i class="bi bi-check2"></i></button>');

//        // Kaydet butonuna tıklama
//        row.find('.save-btn').on('click', function () {
//          var updatedProductName = row.find('.product-name').val();
//          var updatedProductUnit = row.find('.product-unit').val();
//          var updatedProductPrice = row.find('.product-price').val();

//          if (updatedProductName && updatedProductPrice) {
//            // Verileri güncelle
//            table.cell(row, 0).data(updatedProductName).draw();
//            table.cell(row, 1).data(updatedProductUnit).draw();
//            table.cell(row, 2).data(updatedProductPrice).draw();

//            // Kaydet butonunu güncelleme butonuna çevir
//            row.find('.save-btn').replaceWith('<button class="btn update-btn"><i class="bi bi-pencil-square"></i></button>');
//          } else {
//            alert('Lütfen tüm alanları doldurun.');
//          }
//        });
//      });

//      // Silme butonuna tıklama
//      $(rowNode).find('.delete-btn').on('click', function () {
//        var row = $(this).closest('tr');
//        table.row(row).remove().draw();
//      });
//    });
//  } catch (error) {
//    alert('Ürün verileri yüklenirken bir hata oluştu.');
//  }
//}

// Asynchronous function to load units
//async function loadUnits() {
//  try {
//    const response = await $.ajax({
//      url: '/api/Unit/ListUnitsAsync', // API endpoint adresi
//      type: 'GET',
//      dataType: 'json',
//    });

//    unitOptions = ''; // Mevcut verileri temizle
//    response.forEach(function (unit) {
//      unitOptions += `<option value="${unit.id}">${unit.name}</option>`;
//    });
//  } catch (error) {
//    alert('Birim verileri yüklenirken bir hata oluştu.');
//  }
//}