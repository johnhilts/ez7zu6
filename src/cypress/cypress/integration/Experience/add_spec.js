import * as helper from '../Helper/AccountHelper';
import * as common from '../Helper/CommonHelper';

describe('experience', () => {
    context('adding', () => {
        it('can reach the add experience page via the / route', () => {
            cy.visit(`${common.baseUrl}/`)
            cy.get('textarea').should('have.attr', 'placeholder', 'record an experience')
        })
    })

    context('new member', () => {
        it('can add experiences and still see after cycling login', () => {
            const getRandomName = () => {
                let random = Math.floor(Math.random() * Math.pow(10, 8));
                let today = new Date();
                return `${today.getFullYear()}${today.getMonth()}${today.getDate()}${random}`;
            }

            const registerNewUser = (randomName) => {
                cy.visit(`${common.baseUrl}${helper.registerPath}`);
                let name = `Test ${randomName}`;
                let username = `${randomName}@test.com`;
                let password = 'test123';
                cy.get('#txtName').type(name).should('have.value', name);
                cy.get('#txtUserName').type(username).should('have.value', username);
                cy.get('#txtPassword').type(password).should('have.value', password);
                cy.get('#txtConfirmPassword').type(password).should('have.value', password);
                cy.get('button').click();

                return username;
            }

            const addNewExperience = (note) => {
                cy.visit(`${common.baseUrl}/`)
                cy.get('textarea').type(note).should('have.value', note);
                cy.get('button').contains('SAVE').click();
            }

            let randomName = getRandomName();
            let newUsername = registerNewUser(randomName);
            let notes = [1, 2, 3].map(e => `note ${e} from ${newUsername}`)
            notes.map(e => addNewExperience(e))
            helper.logout();
            cy.visit(`${common.baseUrl}${helper.loginPath}`);
            helper.login(newUsername);
            cy.visit(`${common.baseUrl}`);
            cy.get('ul>li')
                .each(function ($el, index, $list) {
                    expect(notes.indexOf($list[index].innerText)).to.be.gt(-1);
                })

        })
    })
})