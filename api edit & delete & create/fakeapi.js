const table=document.getElementById("table").querySelector("tbody");

const xhr=new XMLHttpRequest();
xhr.open("get","https://68d9a7fb90a75154f0dadf34.mockapi.io/api/books/Books");
xhr.onreadystatechange=function(){
    if(xhr.readyState===4){
        const data= JSON.parse(xhr.responseText);
        for(item of data){
            table.innerHTML+=`
            <tr>
             <td>${item.isbn} </td>
            <td>${item.id} </td>
            <td>${item.bookname} </td>
            <td>${item.description} </td>
            <td>${item.price} </td>
            <td>${item.author} </td>
            <td>${item.publishdate} </td>
            <td>${item.available} </td>
              
            <td> <button id="${item.id} " class="del" onclick="deletepost(this)">Delete</button>
          <a href="edit.html?id=${item.id}">Edit</a>

            </td>
        </tr>
            `;
        }
    }
}
xhr.send();

function deletepost(button){
const id=button.id;
const xhr2=new XMLHttpRequest();
const delmsg=document.getElementById("delmsg");
xhr2.open("DELETE", `https://68d9a7fb90a75154f0dadf34.mockapi.io/api/books/Books/${id}`);
xhr2.onreadystatechange=function(){
if(xhr2.readyState===4){
    if(xhr2.status===200){
       delmsg.innerText="Book Deleted ...";
       delmsg.style.color="red";
       button.closest("tr").remove();
    }
}
else {
        delmsg.innerText = "Error Deleting ...";
        delmsg.style.color = "orange";
      }
};
  xhr2.send();
}



