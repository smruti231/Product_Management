const express = require('express');
const sql = require('msnodesqlv8');
const cors = require('cors');
const app = express();
app.use(express.json());
app.use(express.static("public"));
app.use(express.urlencoded({extended:true}));   //Body will be processes as JSON data
app.use(cors());


const driver = 'ODBC Driver 17 for SQL Server';
const server = 'W-674PY03-2';
const dbname = 'SMRUTI_db';
const userid = 'SA';
const pwd = "Password@123456-=";

const conString = `Driver=${driver};Server=${server};Database=${dbname};uid=${userid};pwd=${pwd};`;

const query = "SELECT * FROM tblProduct";

function validationRules(req,res,next){
    const {pName, pPrice, pRating, pStock} = req.body;
    if(pName == ""){
        return res.send("Product Name is must");
    }
    if(pPrice == ""){
        return res.send("Product Price is must");
    }
    if(pRating == ""){
        return res.send("Product Rating is must");
    }
    if(pStock == ""){
        return res.send("Product Stock is must");
    }
    next();
}

//Get the Product by ID
app.get("/Products/:id",(req,res)=>{
    const id = req.params.id;
    const query = `Select * from tblProduct where pId=${id}`;
    sql.query(conString, query,(err,rows)=>{
        res.send(rows[0])
    })
})

app.post("/Products", validationRules, (req,res)=>{
    const body = req.body;
    const query = `Insert into tblProduct values ('${body.pName}', '${body.pPrice}', '${body.pRating}', '${body.pStock}')`;
    sql.query(conString,query,(err, rows)=>{
        if(err){
            res.status(400).send(err.message);
        }
        else{
            res.status(200).send("Product added successfully");
        }
    })
})


//Update Product
app.put("/Products/:id", (req, res)=>{
    const body = req.body;
    const id = req.params.id;
    const query = `Update tblProduct set pName='${body.pName}',pPrice='${body.pPrice}', pRating=${body.pRating}, pStock=${body.pStock} where pId=${id}` ;
    sql.query(conString, query,(err,rows)=>{
        if(err){
            res.status(400).send(err.message);
        }
        else{
            res.status(200).send("Product updated successfully");
        }
    })
} )

app.delete("/Products/:id", (req,res)=>{
    const id = req.params.id;
    const query = "Delete from tblProduct where pId = " +id;
    sql.query(conString,query,(err, rows)=>{
        if(err){
            res.status(400).send(err.message);
        }
        else{
            res.status(200).send("Product deleted successfully");
        }
    })
})


app.get("/Products/search", (req, res) => {
    const criteria = req.query.criteria; // 'name' or 'rating'
    const term = req.query.term.toLowerCase();
    let query = "";
  
    if (criteria == "name") {
      query = `select * from tblProduct where LOWER(pName) LIKE '%${term}%'`;
    } 
    else if (criteria == "rating") {
      const rating = parseFloat(term);
      if (!isNaN(rating)) {
       query = `select * from tblProduct where pRating = ${rating}`;
      }
    }
    
    sql.query(conString, query, (err, rows)=>{
        if(err){
            res.status(400).send(err.message);
        }
        else{
            res.status(200).send("Product deleted successfully");
        }
    });
  });

  app.get("/Products", (req,res)=>{
    sql.query(conString, query, (err,rows) =>{
        if(err){
            res.send(err)
        }else
        res.send(rows);
    })
});

app.listen(1234, ()=>{
    console.log("Server at 1234");
});
