var Books = function(container){
  this._container = container;
  this._http = {
    init: function(callback){
      $.ajax("/Books/Init", { type: "POST", dataType: 'json'}).done(function(res){
        callback(res);
      });
    },
    getBook: function(callback){
      $.ajax("/Books/Get", { type: "GET", data: { }, dataType: 'json'}).done(function(res){
        callback(res);
      });
    },
    search: function(word, callback){
      $.ajax("/Books/Search", { type: "GET", data: { word: word }, dataType: 'json'}).done(function(res){
        callback(res);
      });
    },
    add: function(name, description, category, callback){
      $.ajax("/Books/Add", { type: "POST", data: { 
        name: name,
        description: description,
        category: category
       }, dataType: 'json'}).done(function(res){
        callback(res);
      });
    }
  }

  this.render = function(){
    this._attachEvent();

    this._http.getBook((books) => {
      this._refresh(books);
    });
  }

  this._attachEvent = function(){
    this._container.find("#exec-search").on("click", () => {
      const word = this._container.find("#search-word").val();
      this._http.search(word, (books) => {
        this._refresh(books);
      });
    });
    this._container.find("#env-init").on("click", () => {
      this._http.init((books) => {
        this._refresh(books);
      });
    });
    this._container.find("#exec-add-book").on("click", () => {
      const name = this._container.find("#add-book-name").val();
      const description = this._container.find("#add-book-description").val();
      const category = this._container.find("#add-book-category").val();
      this._http.add(name, description, category, (books) => {
        this._container.find("#modal").modal("hide");
        this._refresh(books);
      });
    });
  }

  this._refresh = function(books){
    this._container.find(".main-body table tbody").html(books.map(r => this._createRowTag(r)).join(""));
  }

  this._createRowTag = function(book){
    return ""
      + "<tr>"
      + " <td>" + book.name + "</td>"
      + " <td>" + book.category + "</td>"
      + " <td>" + book.description + "</td>"
      + "</tr>";
  }

  
}
