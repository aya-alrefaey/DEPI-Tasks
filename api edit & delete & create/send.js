const form=document.getElementById("form");
const msg=document.getElementById("msg");

form.onsubmit=function(e){
    e.preventDefault();
     
    const data={
        isbn:form.isbn.value,
        // id:form.id.value,
        bookname:form.name.value,
        description:form.desc.value,
        price:form.price.value,
        author:form.author.value,
        publishdate:form.pub.value,
        available:form.ava.value,
    
    }

    const xhr =new XMLHttpRequest();
    xhr.open("post","https://68d9a7fb90a75154f0dadf34.mockapi.io/api/books/Books");
    xhr.setRequestHeader("content-type","application/json");
    xhr.onreadystatechange=function(){
        if(xhr.readyState===4){
            if(xhr.status===201){
           msg.innerText="Book Added Successfully ...";
           msg.style.color="green";
           msg.style.textAlign="Center";
              setTimeout(() => {
              window.location.href = "fakeapi.html";
            }, 1000);
            }
        }
        else{
           msg.innerText="Book Is Not Created ..."; 
            msg.style.color="red";
           msg.style.textAlign="Center";
        }
    }

    xhr.send(JSON.stringify(data));

}