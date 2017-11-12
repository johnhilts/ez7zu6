let baseUrl = 'http://localhost:17726';
describe('account', () => {
    context('authentication', () => {
        it('should prevent unauthenticated access of profile', () => {
            let expected = '/account/login';
            cy.visit(`${baseUrl}/profile`).url();
            cy.location().should((location) => {
                let actual = location.pathname.toLowerCase();
                expect(actual).to.eq(expected)
            })
        })
        it('should see profile upon login', () => {
            let expected = `${baseUrl}/profile`;
            cy.visit(expected);
            let username = 'john@test.com';
            let password = 'test123';
            cy.get('#txtUserName').type(username).should('have.value', username);
            cy.get('#txtPassword').type(password).should('have.value', password);
            cy.get('button').click();
            cy.location().should((location) => {
                let actual = location.href.toLowerCase(); 
                expect(actual).to.eq(expected)
            })
        })
    })
});
