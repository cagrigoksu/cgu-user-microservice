# cgu-user-microservice.git

#### GET
/api/User/get-user-profile/{userId}
- `param`: userId 
- `return`: Ok(), BadRequest()

#### POST
/api/User/login
- `param`: LoginModel 
- `return`: Ok(), BadRequest()

#### POST
/api/User/logon
- `param`: LogonModel 
- `return`: Ok(), BadRequest()

#### POST
/api/User/add-user-profile
- `param`: UserProfileDataModel 
- `return`: Ok(), BadRequest()

#### POST
/api/User/edit-user-profile
- `param`: UserProfileDataModel 
- `return`: Ok(), BadRequest()

