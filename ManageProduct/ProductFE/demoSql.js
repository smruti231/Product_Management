const express = require('express');
const sql = require('msnodesqlv8');
const cors = require('cors');
const app = express();

app.use(express.json());
app.use(express.static("public"));
app.use(express.urlencoded({ extended: true }));
app.use(cors());

const driver = 'ODBC Driver 17 for SQL Server';
const server = 'W-674PY03-2';
const dbname = 'SMRUTI_db';
const userid = 'SA';
const pwd = "Password@123456-=";

const conString = `Driver=${driver};Server=${server};Database=${dbname};uid=${userid};pwd=${pwd};`;

const query = "SELECT * FROM tblProduct";

function validationRules(req, res, next) {
    const { pName, pPrice, pRating, pStock } = req.body;
    if (pName == "") {
        return res.send("Product Name is required");
    }
    if (pPrice == "") {
        return res.send("Product Price is required");
    }
    if (pRating == "") {
        return res.send("Product Rating is required");
    }
    if (pStock == "") {
        return res.send("Product Stock is required");
    }
    next();
}

// Get the Product by ID
app.get("/Products/:id", (req, res) => {
    const id = req.params.id;
    const query = `SELECT * FROM tblProduct WHERE pId=${id}`;
    sql.query(conString, query, (err, rows) => {
        if(err)
            res.send(err);
        else
            res.send(rows[0]);
    });
});

// Add a new Product
app.post("/Products", validationRules, (req, res) => {
    const body = req.body;
    const query = `INSERT INTO tblProduct (pName, pPrice, pRating, pStock) VALUES ('${body.pName}', '${body.pPrice}', '${body.pRating}', '${body.pStock}')`;
    sql.query(conString, query, (err, rows) => {
        if (err) {
            res.status(400).send(err.message);
        } else {
            res.status(200).send("Product added successfully");
        }
    });
});

// Update Product
app.put("/Products/:id", (req, res) => {
    const body = req.body;
    const id = req.params.id;
    const query = `UPDATE tblProduct SET pName='${body.pName}', pPrice='${body.pPrice}', pRating=${body.pRating}, pStock=${body.pStock} WHERE pId=${id}`;
    sql.query(conString, query, (err, rows) => {
        if (err) {
            res.status(400).send(err.message);
        } else {
            res.status(200).send("Product updated successfully");
        }
    });
});

// Delete Product
app.delete("/Products/:id", (req, res) => {
    const id = req.params.id;
    const query = `DELETE FROM tblProduct WHERE pId=${id}`;
    sql.query(conString, query, (err, rows) => {
        if (err) {
            res.status(400).send(err.message);
        } else {
            res.status(200).send("Product deleted successfully");
        }
    });
});


app.get("/Products/search/:criteria/:term", (req, res) => {
    const criteria = req.params.criteria; // 'name' or 'rating'
    const term = req.params.term.toLowerCase();
    let query = "";
    if (criteria == "name") {
        query = `SELECT * FROM tblProduct WHERE LOWER(pName) LIKE '%${term}%'`;
    } else if (criteria == "rating") {
        const rating = parseInt(term);
        if (!isNaN(rating)) {
            query = `SELECT * FROM tblProduct WHERE pRating = ${rating}`;
        }
    }
    console.log(query)
    sql.query(conString, query, (err, rows) => {
        if(err){
            console.error("Searching error: ", err);
            res.status(400).send(err.message);
        }
        else{
            res.send(rows);
        }
    });
});


app.get("/Products", (req, res) => {
    sql.query(conString, query, (err, rows) => {
        if (err) {
            res.send(err);
        } else {
            res.send(rows);
        }
    });
});


app.get("/Products/sort/:criteria", (req,res)=>{
    const criteria = req.params.criteria;
    let query="";

    if(criteria == "name"){
        query = "Select * from tblProduct ORDER BY pName";
    }
    else if(criteria == "price"){
        query = "Select * from tblProduct ORDER BY pPrice";
    }
    else if(criteria == "rating"){
        query = "Select * from tblProduct ORDER BY pRating DESC";
    }
    else if(criteria == "stock"){
        query = "Select * from tblProduct ORDER BY pStock";
    }

    sql.query(conString, query, (err,rows)=>{
        if(err){
            console.error("Sorting error: ", err);
            res.status(400).send(err.message);
        }
        else{
            // res.status(200).send("Sorted successfully");
            res.send(rows)
        }
    })
})

app.listen(1234, () => {
    console.log("Server is running on port 1234");
});
