$(document).ready(function () {
  const urlPath = window.location.pathname;

  // URL'den ID'yi almak (URL formatı: /api/Products/UpdateProductPage/{id})
  const productId = urlPath.split('/').pop(); 
  $.ajax({
    url: `/api/Products/GetProductDetails/${productId}`, // API endpoint
    type: "GET",
    dataType: "json",
    success: function (product) {
      // Ürün bilgilerini form alanlarına yerleştir
      $('#kategori').val(product.productCategory); // Kategori seç
      $('#urunAdi').val(product.productName); // Ürün adını yaz
      $('#fiyat').val(product.productPrice); // Fiyatı yaz

      // Hammaddeleri formda göster
      product.rawMetarials.forEach(function (material, index) {
        if (index === 0) {
          // İlk hammaddeyi ekle
          $('#hammaddeler').html(`
                        <div class="d-flex align-items-center mb-2 hammadde-row" data-id="${index + 1}">
                            <button type="button" class="btn btn-sm me-2 add-hammadde"><i class="bi bi-plus"></i></button>
                            <select class="form-select me-2" style="width: 50%;">
                                <option selected>${material.name}</option>
                                <option value="1">Hammadde 1</option>
                                <option value="2">Hammadde 2</option>
                                <option value="3">Hammadde 3</option>
                            </select>
                            <select class="form-select me-2 birim-alan" style="width: 10%;">
                                <option selected>${material.unit}</option>
                                <option value="1">Kg</option>
                                <option value="1">Gr</option>
                                <option value="2">L</option>
                                <option value="3">Ml</option>
                            </select>
                            <input type="number" class="form-control me-2 miktar-alan" value="${material.quantity}" placeholder="Miktar" style="width: 25%;">
                            <button type="button" class="btn btn-sm remove-hammadde"><i class="bi bi-dash"></i></button>
                        </div>
                    `);
        } else {
          // Yeni hammaddeyi ekle
          const newRow = `
                        <div class="d-flex align-items-center mb-2 hammadde-row" data-id="${index + 1}">
                            <button type="button" class="btn btn-success btn-sm me-2 add-hammadde"><i class="bi bi-plus"></i></button>
                            <select class="form-select me-2" style="width: 50%;">
                                <option selected>${material.name}</option>
                                <option value="1">Hammadde 1</option>
                                <option value="2">Hammadde 2</option>
                                <option value="3">Hammadde 3</option>
                            </select>
                            <select class="form-select me-2 birim-alan" style="width: 10%;">
                                <option selected>${material.unit}</option>
                                <option value="1">Kg</option>
                                <option value="1">Gr</option>
                                <option value="2">L</option>
                                <option value="3">Ml</option>
                            </select>
                            <input type="number" class="form-control me-2 miktar-alan" value="${material.quantity}" placeholder="Miktar" style="width: 25%;">
                            <button type="button" class="btn btn-danger btn-sm remove-hammadde"><i class="bi bi-dash"></i></button>
                        </div>
                    `;
          $('#hammaddeler').append(newRow);
        }
      });

      // Select2'yi tekrar başlat
      $('.form-select').select2();
    },
    error: function () {
      alert("Ürün detayları alınırken bir hata oluştu!");
    }
  });

  $('.form-select').select2();
  let rowId = 1; // Başlangıç ID'si

  // Hammadde ekleme
  $(document).on('click', '.add-hammadde', function () {
    rowId++;
    const newRow = `<div class="d-flex align-items-center mb-2 hammadde-row" data-id="${rowId}">
                              <button type="button" class="btn btn-success btn-sm me-2 add-hammadde"><i class="bi bi-plus"></i></button>
                              <select class="form-select me-2" style="width: 50%;">
                                  <option selected>Hammadde Seçin</option>
                                  <option value="1">Hammadde 1</option>
                                  <option value="2">Hammadde 2</option>
                                  <option value="3">Hammadde 3</option>
                              </select>
                              <select class="form-select me-2 birim-alan" style="width: 10%;">
                                <option selected>Birim</option>
                                <option value="1">Kg</option>
                                <option value="1">Gr</option>
                                <option value="2">L</option>
                                <option value="3">Ml</option>
                              </select>
                              <input type="number" class="form-control me-2 miktar-alan" placeholder="Miktar" style="width: 25%;">
                              <button type="button" class="btn btn-danger btn-sm remove-hammadde"><i class="bi bi-dash"></i></button>
                          </div>`;
    $('#hammaddeler').append(newRow);
    updateRowIds();
    $('.form-select').select2();
  });

  // Hammadde silme
  $(document).on('click', '.remove-hammadde', function () {
    if ($('.hammadde-row').length > 1) {
      $(this).closest('.hammadde-row').remove();
      updateRowIds();
    } else {
      alert('En az bir hammadde ekli olmalıdır.');
    }
  });

  // Satır ID'lerini güncelle
  function updateRowIds() {
    rowId = 0;
    $('#hammaddeler .hammadde-row').each(function () {
      rowId++;
      $(this).attr('data-id', rowId);
    });
  }
});