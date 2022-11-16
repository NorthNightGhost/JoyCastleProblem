    void Solution(TreeNode* root) {
        vector<vector<int>> result;
        queue<TreeNode*> que;
        if(root != NULL) que.push(root);
        cout<<"[";
        bool flag=true;
        while(!que.empty()) {
            int size = que.size();
            vector<int> vec;
            for(int i = 0; i < size; i++) {

                TreeNode* node = que.front();
                que.pop();
                if(i==0){
	                if(!flag)	cout<<",";
                    flag=false;
                    cout<<node->val;
                }

                vec.push_back(node->val);
                if(node->left != NULL) que.push(node->left);
                if(node->right != NULL) que.push(node->right);
            }
            result.push_back(vec);
        }
        cout<<"]"<<endl;
    }