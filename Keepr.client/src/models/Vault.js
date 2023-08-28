export class Vault {
  constructor(data) {
    this.description = data.description
    this.name = data.name
    this.img = data.img
    this.creator = data.creator
    this.isPrivate = data.isPrivate
    this.id = data.id
  }
}

// {
//   "description": "Here lies all of the things that have wronged me",
//     "name": "Cool Ideas",
//       "img": "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=500&h=400&q=60",
//         "creatorId": "64e8e1e13d83d911ace899dd",
//           "isPrivate": false,
//             "creator": {
//     "name": "gooduser@good.user",
//       "picture": "https://s.gravatar.com/avatar/47fbd3c340b6871ba0220ec3f2c185ce?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fgo.png",
//         "id": "64e8e1e13d83d911ace899dd",
//           "createdAt": "2023-08-25T17:18:59",
//             "updatedAt": "2023-08-25T17:18:59"
//   },
//   "id": 1,
//     "createdAt": "2023-08-25T19:18:49",
//       "updatedAt": "2023-08-25T19:18:49"
// }