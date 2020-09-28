var Books = function(container){
  this._container = container;

  this.render = function(){
    this._container.html(this._createBaseTag());
    this._refresh();
  }

  this._refresh = function(){
    var self = this;
    $.ajax("/Books/Get",
      {
        type: "GET",
        data: { },
        dataType: 'json'
      }
    ).done(function(res){
      var tag = "";
      $.each(res, function(){
        tag += self._createRowTag(this);
      })
      self._container.find(".table tbody").html(tag);
    })
  }

  this._createBaseTag = function(){
    return ""
      + "<table class='table'>"
      + " <thead>"
      + "   <tr>"
      + "     <td>タイトル</td>"
      + "     <td>説明</td>"
      + "   </tr>"
      + " </thead>"
      + " <tbody>"
      + " </tbody>"
      + "</table>"
  }

  this._createRowTag = function(book){
    return ""
      + "<tr>"
      + " <td>" + book.name + "</td>"
      + " <td>" + book.description + "</td>"
      + "</tr>";
  }
}
