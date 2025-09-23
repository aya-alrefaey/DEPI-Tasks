
const table=document.getElementById("table").querySelector("tbody");

const xhr=new XMLHttpRequest();
xhr.open("get","https://jsonplaceholder.typicode.com/posts");
xhr.onreadystatechange=function(){
    if(xhr.readyState==4){
        const data= JSON.parse(xhr.responseText);
        for(item of data){
            table.innerHTML+=`
            <tr>
            <td>${item.userId} </td>
            <td>${item.id} </td>
            <td>${item.title} </td>
            <td>${item.body} </td>
            </tr>
            `;
        }
    }
}
xhr.send();