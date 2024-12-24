$(document).ready(function () {
  // DataTable başlat
  const table = $('#productTable').DataTable({
    "responsive": true,
    data: [],
    columns: [
      { "data": 'productName', title: 'Ürün Adı' },
      { "data": 'productType', title: 'Ürün Cinsi' },
      { "data": 'productImg', title: 'Ürün Resmi' },
      { "data": 'productPrice', title: 'Ürün Fiyatı' },
      {
        "data": 'id', // 'id' alanı JSON'dan geliyor
        title: 'İşlem',
        render: function (data) {
          // İşlem sütununa düzenle ve sil butonlarını ekle 
          return `
            <a href="#" class="btn show-btn" data-bs-toggle="modal" data-bs-target="#productDetailModal" onclick="showModal(${data})">
              <i class="bi bi-eye"></i>
            </a>
            <a href="UpdateProductPage/${data}" class="btn edit-btn"><i class="bi bi-pencil-square"></i></a>
            <a href="DeleteProduct/${data}" class="btn delete-btn"><i class="bi bi-trash"></i></a>
          `;
        }
      }
    ]
  });

  // AJAX isteği
  $.ajax({
    url: "/api/Products/ListProductAsync", // API endpoint'iniz
    type: "GET",
    dataType: "json",
    success: function (data) {
      // Gelen veri doğrudan bir liste olduğundan, DataTable'a ekleyebiliriz
      table.clear().rows.add(data).draw(); // Veriyi ekle ve tabloyu yeniden çiz
    },
    error: function (xhr, status, error) {
      alert("Ürünler yüklenirken bir hata oluştu!");
    }
  });
});

//function showModal(productId) {
//  // AJAX ile ürün detaylarını al
//  $.ajax({
//    url: `/api/Products/GetProductDetails/${productId}`, // API endpoint
//    type: "GET",
//    dataType: "json",
//    success: function (product) {
//      // Modal içeriğini doldur
//      $('#modalProductName').text(product.productName);
//      $('#modalProductType').text(product.productType);
//      $('#modalProductPrice').text(product.productPrice);
//      $('#modalProductImg').attr('src', product.productImg);

//      // Modal'ı göster
//      $('#productDetailModal').modal('show');
//    },
//    error: function (xhr, status, error) {
//      console.error("Hata Detayı:", status, error);
//      alert("Ürün detayları alınırken bir hata oluştu!");
//    }
//  });
//}

function showModal(productId) {
  // AJAX ile ürün detaylarını al
  $.ajax({
    url: `/api/Products/GetProductDetails/${productId}`, // API endpoint
    type: "GET",
    dataType: "json",
    success: function (product) {
      // Ürün detaylarını doldur
      $('#modalProductName').text(product.productName); // Ürün adı
      $('#modalCategory').text(product.productCategory); // Kategori
      $('#modalProductPrice').text(product.productPrice); // Fiyat
      $('#modalProductImg').attr('src', product.productImg); // Ürün resmi

      // Hammaddeler tablosunu doldur
      const rawMaterialsTable = $('#modalRawMaterials');
      rawMaterialsTable.empty(); // Önce eski verileri temizle
      product.rawMetarials.forEach(rawMetarials => {
        rawMaterialsTable.append(`
          <tr>
            <td>${rawMetarials.name}</td>
            <td>${rawMetarials.unit}</td>
            <td>${rawMetarials.quantity}</td>
          </tr>
        `);
      });

      // Modal'ı göster
      $('#productDetailModal').modal('show');
    },
    error: function (xhr, status, error) {
      console.error("Hata Detayı:", status, error);
      alert("Ürün detayları alınırken bir hata oluştu!");
    }
  });
}

