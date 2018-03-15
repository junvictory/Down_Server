var express = require('express');
var index = require('./routes/index.js');
var ajax = require('./routes/ajax.js')

//express templete set
var app = express();
app.set('view engine', 'jade'); // templeate engine jada use
app.set('views', './views'); //views Folder
app.use('/resources',express.static('public')); 
app.use('/files',express.static('files')); 


// app.use(express.static('css')); //scripts정적 파일 사용
app.locals.pretty = true; 



app.use('/', index);
app.use('/ajax', ajax);



app.use((req, res, next) => { // 404 처리 부분
    
    res.status(404).render('404');
    
});
app.use((err, req, res, next) => { // 에러 처리 부분
    console.error(err.stack); // 에러 메시지 표시
    res.status(500).send('Server Error'); // 500 상태 표시 후 에러 메시지 전송
});


app.listen(80, function () {
    console.log('Connection');
});
