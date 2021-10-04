import { IClient } from "../interfaces/IClient"
import { ICreateClientDto } from "../interfaces/ICreateClientDto"
import { IPatchOp } from "../interfaces/IPatchOp"

const def:IClient[] = [{id: 'Error', name:'Error', age:'Error',sex:'Error'}]
const baseAddress:string = "https://192.168.0.109:5001/api/";

export class UserService {

    static async getUsers(): Promise<IClient[]> {
        const response = await fetch(baseAddress + 'users')
        .then((r) => r.ok?r.json().then((u:IClient[]) => u):def)
        .catch(() => def)

        return response
    }

    static async searchUsers(input:string): Promise<IClient[]> {
        const response = await fetch(baseAddress + `users?name=${input}`)
        .then((r) => r.ok?r.json().then((u:IClient[]) => u):def)
        .catch(() => def)

        return response
    }

    static async createUser(client:ICreateClientDto):Promise<number> {
        const response = await fetch(baseAddress + `users`, {
            method:'POST', 
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
            body:JSON.stringify({
                name:client.name, 
                birthDate: client.dateBirth, 
                sex: client.sex})
            }).then((r) => r.status)

        return response
    }

    static async deleteUser(input:string): Promise<number> {
        const response = await fetch(baseAddress + `users/${input}`, {
            method:'DELETE', 
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
            }).then((r) => r.status)

        return response
    }

    static async editUser(id:string, operations:IPatchOp[]):Promise<number> {
        const response = await fetch(baseAddress + `users/${id}`, {
            method:'PATCH',
            headers: {
                'Accept': 'application/json-patch+json',
                'Content-Type': 'application/json-patch+json'
              },
            body:JSON.stringify(
                operations.map((x => x))
            )
            }).then((r) => r.status)

        return response
    }

}