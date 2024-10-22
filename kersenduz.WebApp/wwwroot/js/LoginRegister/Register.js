function RegisterUpdate() {
    var model = {
        Name: "Ugur",
        Email: "ugur@ugur.com",
        Password: "123",
        UserName: "uggurr",
        CreatedDate: new Date().toISOString(),
        ModifiedDate: new Date().toISOString(),
        IsActive: true
    };

    $.ajax({
        url: "/LoginRegister/Register",
        type: "POST",
        dataType: "json",
        data: model,
        success: function (data) {
            console.log("Kullanıcı kaydı başarıyla tamamlandı:");
            console.log(data);
            alert("Kayıt başarılı: " + data.Name);
        }
    });
}
