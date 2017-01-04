angular.module("app.common", [])
    .service("Common",  function() {
        this.AddEntity = AddEntity
    }
);

function AddEntity(newEntity, entities) {
    newEntity.$save(function (data, putResponseHeaders) {
        entities.push(newEntity);
    }, function (res) {
        var modelState = res.data.ModelState;
        var l1, l2 = ''
        console.log('Kunde inte skapa ', res.data);

        angular.forEach(modelState, function(outervalue, outerkey) {
            var fields = outervalue;
            angular.forEach(fields, function(value, key) {
                console.log(outerkey + ': ' + value);
            }, l1);
        }, l2);

    });
    return null;
};