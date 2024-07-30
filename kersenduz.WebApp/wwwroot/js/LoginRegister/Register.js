function RegisterUpdate() {
    //var model = {
    //    Name: "Ugur",
    //    Email: "ugur@ugur.com",
    //    Password: "123",
    //    UserName: "uggurr",
    //    CreatedTime: new Date().toISOString(),
    //    UpdatedTime: new Date().toISOString()
    //};

    $.ajax({
        url: "/LoginRegister/Register",
        type: "GET",
        dataType: "json",
        success: function (data) {
            console.log("Kullanıcı kaydı başarıyla tamamlandı:");
            console.log(data);
            alert("Kayıt başarılı: " + data.Name);
        },
        error: function (xhr, status, error) {
            console.error("Kayıt sırasında hata oluştu:", status, error);
            alert("Kayıt sırasında bir hata oluştu. Lütfen tekrar deneyin.");
        }
    });
}
