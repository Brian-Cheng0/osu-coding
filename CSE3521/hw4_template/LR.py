import argparse
import math
from mpl_toolkits.mplot3d import Axes3D
import numpy as np
import os
import os.path as osp
import matplotlib.pyplot as plt

def linear_regression(X, Y):
    """
    Input:
        X: a N-by-D matrix (numpy array) of the input data
        Y: a N-by-1 matrix (numpy array) of the label
    Output:
        p: the linear parameter vector. Please represent it as a D-by-1 matrix (numpy array).
    Useful tool:
        1. np.matmul: for matrix-matrix multiplication
        2. the builtin "reshape" and "transpose()" functions of a numpy array
        3. np.linalg.inv: for matrix inversion
    """

    A = np.copy(X) # We're using vectors (rows of X) as inputs values as in slide 24,
                   # so X is literally our A matrix

    ### Your job starts here ###

    inversion = np.linalg.inv(np.matmul(A.transpose(),A))
    p = np.matmul(inversion,np.matmul(A.transpose(),Y))

    ### Your job ends here ###
    return p

def polynomial_regression(X, Y, degree):
    """
    Input:
        X: a N-by-1 matrix (numpy array) of the input data
        Y: a N-by-1 matrix (numpy array) of the label
        degree: non-negative integer as degree of polynomial to fit (yes, it can be zero!)
    Output:
        p: the linear parameter vector. Please represent it as a D-by-1 matrix (numpy array).
           p[0] should be the constant term, p[1] linear term, p[2] quadratic term, etc.
    Useful tool:
        1. ** operator
        2. if you want to make this really easy, numpy's "broadcasting" concept will help
    """

    ### Your job starts here ###
    i = 0
    A = np.zeros((X.shape[0], degree + 1))
    while i < X.shape[0]:
        for j in range(degree + 1):
          A[i][j] = X[i] ** j
        i+=1

    ### Your job ends here ###
    
    return linear_regression(A,Y) #Once you the have the A matrix pass it along to your linear reg. function

##############################################################################

## Data loader and data generation functions
def data_loader(args):
    """
    Output:
        X: the data matrix (numpy array) of size 1-by-N
        Y: the label matrix (numpy array) of size N-by-1
    """
    if args.data == "linear":
        print("Using linear")
        X, Y = data_linear()
    elif args.data == "quadratic":
        print("Using quadratic")
        X, Y = data_quadratic()
    else:
        print("Using simple")
        X, Y = data_simple()
    return X, Y

def data_X(N,min,max,sigma,unordered=True):
    X=np.random.normal(size=N)*sigma+1 #spacing semi-gaussian
    X=np.abs(np.mod(X+1,2)-1)+1 #mirror
    X[0]=0
    X=np.cumsum(X) #totals
    X=(X/X[-1]*(max-min))+min #linmap
    if unordered:
      X=np.random.permutation(X)
    return X

def data_linear():
    if not osp.isfile(r"dataset_linear.npz"):
      N=30
      Ntst=math.floor(N*0.3)
      Ntrn=N-Ntst
      X=np.concatenate((data_X(Ntrn,0,1,1),data_X(Ntst+2,0,1,1,unordered=False)[1:-1])).reshape(-1,1)
      Yextent=np.random.rand(2)*5-2
      while abs(Yextent[0]-Yextent[1])<0.2:
        Yextent=np.random.rand(2)*5-2
      Y=Yextent[0]*X+Yextent[1]*(1-X)
      Y=Y+np.random.normal(scale=abs(Yextent[0]-Yextent[1])*0.1,size=N).reshape(-1,1)
      np.savez(r"dataset_linear.npz", X = X, Y = Y)
      del X, Y
    data = np.load(r"dataset_linear.npz")
    X = data['X']
    Y = data['Y']
    return X, Y


def data_quadratic():
    if not osp.isfile(r"dataset_quadratic.npz"):
      N=30
      Ntst=math.floor(N*0.3)
      Ntrn=N-Ntst
      X=np.concatenate((data_X(Ntrn,0,1,1),data_X(Ntst+2,0,1,1,unordered=False)[1:-1])).reshape(-1,1)
      rr=np.random.rand(4)
      pp=rr[0]*0.5+0.25
      rt=pp+np.array([-1,1])*(0.25+rr[1]*0.1)
      Y=(X-rt[0])*(X-rt[1])*(rr[2]*0.4+0.9)*math.copysign(1,rr[3]-0.5)
      Y=Y+np.random.normal(scale=(np.max(Y)-np.min(Y))*0.1,size=N).reshape(-1,1)
      np.savez(r"dataset_quadratic.npz", X = X, Y = Y)
      del X,Y
    data = np.load(r"dataset_quadratic.npz")
    X = data['X']
    Y = data['Y']
    return X, Y


def data_simple():
    N = 20
    X = np.linspace(0.0, 10.0, num=N).reshape(N, 1)
    Y = np.linspace(1.0, 3.0, num=N).reshape(N, 1)
    return X, Y


## Displaying the results
def display_LR(p, X_trn, Y_trn, X_tst, Y_tst, save=False):
    deg = p.shape[0]-1
    x_min = np.min(X_trn)
    x_max = np.max(X_trn)
    x = np.linspace(x_min-0.05, x_max+0.05, num=1000).reshape(-1,1)
    YY = np.matmul(x**np.arange(deg+1).reshape(1,-1), p)
    plt.plot(x.reshape(-1), YY.reshape(-1), color='black', linewidth=3)
    plt.scatter(X_trn.reshape(-1), Y_trn.reshape(-1), c='red')
    plt.scatter(X_tst.reshape(-1), Y_tst.reshape(-1), c='blue')
    if save:
        plt.savefig(args.data + '_' + str(args.polynomial) + '.png', format='png')
        np.savez('Results_' + args.data + '_' + str(args.polynomial) + '.npz', p=p, X_trn=X_trn, X_tst=X_tst, Y_trn=Y_trn, Y_tst=Y_tst)
    plt.show()
    plt.close()


## auto_grader
def auto_grade(p):
    print("In auto grader!")
    if p.ndim != 2:
        print("Wrong dimensionality of w")
    else:
        if p.shape[0] != 2 or p.shape[1] != 1:
            print("Wrong shape of p")
        else:
            if sum((p - [[1],[2.00000000e-01]]) ** 2) < 10 ** -6:
                print("Correct p")
            else:
                print("Incorrect p")


## Main function
def main(args):

    if args.auto_grade:
        args.data = "simple"
        args.polynomial = int(1)
        args.display = False
        args.save = False

    ## Loading data
    X, Y = data_loader(args) # X: the N-by-1 data matrix (numpy array); Y: the N-by-1 label vector

    ## Setup (separate to train and test)
    N = X.shape[0]  # number of data instances of X
    X_test = X[int(0.7*N):,:]
    X_trn = X[:int(0.7*N),:]
    Y_test = Y[int(0.7 * N):, :]
    Y_trn = Y[:int(0.7 * N), :]

    # Running LR
    p = polynomial_regression(X_trn, Y_trn,args.polynomial)
    print("p: ", p.reshape(-1))

    # Evaluation
    training_error = np.mean((np.matmul(X_trn**np.arange(args.polynomial+1).reshape(1,-1),p) - Y_trn) ** 2)
    print("Training mean square error: ", training_error)
    test_error = np.mean((np.matmul(X_test**np.arange(args.polynomial+1).reshape(1,-1),p) - Y_test) ** 2)
    print("Test mean square error: ", test_error)

    if args.display:
        display_LR(p, X_trn, Y_trn, X_test, Y_test, save=args.save)

    if args.auto_grade:
        auto_grade(p)


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Running linear regression (LR)")
    parser.add_argument('--data', default="linear", type=str)
    parser.add_argument('--polynomial', default=1, type=int)
    parser.add_argument('--display', action='store_true', default=False)
    parser.add_argument('--save', action='store_true', default=False)
    parser.add_argument('--auto_grade', action='store_true', default=False)
    args = parser.parse_args()
    main(args)
