form = document.forms[0];
form.addEventListener("submit", function(event) {
    event.preventDefault();
    new FormData(form);
  });
form.addEventListener("formdata", event => {
    // event.formData grabs the object
    const data = event.formData;

  // get the data
    const entries = [...data.entries()];
    let mail = entries[0][1];
    let passwd = entries[1][1];
    if(mail.length ==0){
        Swal.fire({
            icon: 'error',
            title: 'Ошибка',
            text: 'Некорректная почта',
            footer: '<a href="">Why do I have this issue?</a>'
          })
        return;
    }
    if(passwd.length <8){
        Swal.fire({
            icon: 'error',
            title: 'Ошибка',
            text: 'Некорректный пароль'
          })
        return;
    }
    Swal.fire({
        icon: 'success',
        title: 'Успех',
        text: mail + " " + passwd
      })
    console.log(entries);
});