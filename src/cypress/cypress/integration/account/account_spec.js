import * as helper from '../Helper/AccountHelper';
import * as common from '../Helper/CommonHelper';

describe('account', () => {
    context('authentication', () => {
        it('should prevent unauthenticated access of profile', () => {
            cy.visit(`${common.baseUrl}${helper.authenticationRequiredPath}`).url();
            cy.location().should((location) => {
                let expected = helper.loginPath;
                let actual = location.pathname.toLowerCase();
                expect(actual).to.eq(expected)
            })
        })
        it('should see profile upon login', () => {
            let expected = `${common.baseUrl}${helper.authenticationRequiredPath}`;
            cy.visit(expected);
            helper.login();
            cy.location().should((location) => {
                let actual = location.href.toLowerCase(); 
                expect(actual).to.eq(expected)
            })
        })
        it('cannot access profile after logout', () => {
            cy.visit(`${common.baseUrl}${helper.loginPath}`);
            helper.login();
            cy.get('a.glyphicon-log-out').click()
            let unaccessable = `${common.baseUrl}${helper.authenticationRequiredPath}`;
            cy.visit(unaccessable);
            cy.location().should((location) => {
                let expected = helper.loginPath;
                let actual = location.pathname.toLowerCase(); 
                expect(actual).to.eq(expected)
            })
        })
    })
});
