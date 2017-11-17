import * as helper from '../Helper/AccountHelper';

let baseUrl = 'http://localhost:17726';
describe('account', () => {
    context('authentication', () => {
        it('should prevent unauthenticated access of profile', () => {
            cy.visit(`${baseUrl}${helper.authenticationRequiredPath}`).url();
            cy.location().should((location) => {
                let expected = helper.loginPath;
                let actual = location.pathname.toLowerCase();
                expect(actual).to.eq(expected)
            })
        })
        it('should see profile upon login', () => {
            let expected = `${baseUrl}${helper.authenticationRequiredPath}`;
            cy.visit(expected);
            helper.login();
            cy.location().should((location) => {
                let actual = location.href.toLowerCase(); 
                expect(actual).to.eq(expected)
            })
        })
        it('cannot access profile after logout', () => {
            cy.visit(`${baseUrl}${helper.loginPath}`);
            helper.login();
            cy.contains('a', 'Logout').click()
            let unaccessable = `${baseUrl}${helper.authenticationRequiredPath}`;
            cy.visit(unaccessable);
            cy.location().should((location) => {
                let expected = helper.loginPath;
                let actual = location.pathname.toLowerCase(); 
                expect(actual).to.eq(expected)
            })
        })
    })
});
