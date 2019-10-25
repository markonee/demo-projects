var godine = null;
var klinac = null;

document.getElementById("pokreniAjax").addEventListener("click", function () {
    dobijToken('NXeWYaPRXUCLzNC8Sr', '4vAcQu5QvKV7Sx9seGkGdMrG3bYbneLt').then((token) => {
        var bratmoj = 'https://api.bitbucket.org/2.0/repositories?role=contributor&q=(name~"2018" OR name~"rma")'

        console.log(bratmoj);

        $.ajax({
            method: 'GET',
            url: bratmoj,
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success(data) {
                console.log(data);
            }
        })
    }).catch((e) => {
        // throw e;
        console.log(e);
    })
}, false);

function dobijToken(key, secret) {

    return new Promise((resolve, reject) => {
        $.ajax({
            method: 'POST',
            url: 'https://bitbucket.org/site/oauth2/access_token',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': 'Basic ' + btoa(key + ':' + secret)
            },
            data: {
                client: {
                    key: key,
                    secret: secret
                },
                grant_type: 'client_credentials'
            },
            success(data) {
                resolve(data.access_token);
            },
            error(data) {
                reject("Desio se belaj...");
            }
        })
    });

}